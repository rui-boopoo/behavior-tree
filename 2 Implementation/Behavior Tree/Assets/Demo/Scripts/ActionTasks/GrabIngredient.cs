using Boopoo.BehaviorTrees;
using System;
using UnityEngine;

/// <summary>
/// A task for grabbing a specific type of ingredient from an IngredientTable and storing it in the blackboard.
/// </summary>
public class GrabIngredient<TTable> : Task where TTable : Table
{
    private string _tableKey;
    private string _itemKey;
    private bool _grabbed;

    /// <summary>
    /// Initializes a new instance of the GrabIngredient task.
    /// </summary>
    /// <param name="tableKey"></param>
    /// <param name="itemKey">The key under which the grabbed ingredient will be stored in the blackboard.</param>
    public GrabIngredient(string tableKey, string itemKey)
    {
        if (string.IsNullOrEmpty(itemKey))
            throw new ArgumentNullException(nameof(itemKey), "Store key cannot be null or empty.");
        _itemKey = itemKey;
        _tableKey = tableKey;
    }

    /// <summary>
    /// Called each time the task is updated. Tries to grab an ingredient of type T from the IngredientTable.
    /// </summary>
    /// <param name="agent">The agent performing the task.</param>
    /// <param name="blackboard">The blackboard used for storing and retrieving data.</param>
    protected override void OnUpdate(Component agent, Blackboard blackboard)
    {
        if (_grabbed)
        {
            EndAction();
            return;
        }

        if (!blackboard.TryRead(_tableKey, out TTable table))
            throw new InvalidOperationException($"'{_tableKey}' not found in blackboard.");

        Component item = table.Interact(out bool interactSucceed);

        if (!interactSucceed)
            throw new InvalidOperationException($"Failed to interact with the {table.GetType().Name}.");

        blackboard.Write(_itemKey, (IngredientHandler)item);
        _grabbed = true;
        EndAction();
    }
}