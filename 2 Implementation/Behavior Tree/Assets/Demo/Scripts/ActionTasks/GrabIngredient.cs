using Boopoo.BehaviorTrees;
using System;
using UnityEngine;

/// <summary>
/// A task for grabbing a specific type of ingredient from an IngredientTable and storing it in the blackboard.
/// </summary>
public class GrabIngredient<TTable> : Task where TTable : Table
{
    private string _tableName;
    private string _storeKey;
    private bool _grabbed;

    /// <summary>
    /// Initializes a new instance of the GrabIngredient task.
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="storeKey">The key under which the grabbed ingredient will be stored in the blackboard.</param>
    public GrabIngredient(string tableName, string storeKey)
    {
        if (string.IsNullOrEmpty(storeKey))
            throw new ArgumentNullException(nameof(storeKey), "Store key cannot be null or empty.");
        _storeKey = storeKey;
        _tableName = tableName;
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

        if (!blackboard.TryRead(_tableName, out TTable table))
            throw new InvalidOperationException($"'{_tableName}' not found in blackboard.");

        Component item = table.Interact(out bool interactSucceed);

        if (!interactSucceed)
            throw new InvalidOperationException($"Failed to interact with the {table.GetType().Name}.");

        blackboard.Write(_storeKey, (IngredientHandler)item);
        _grabbed = true;
        EndAction();
    }
}