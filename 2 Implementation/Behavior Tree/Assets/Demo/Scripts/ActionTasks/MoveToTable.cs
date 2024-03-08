using Boopoo.BehaviorTrees;
using System;
using UnityEngine;

/// <summary>
/// Represents a task that moves an agent to a specified table. The target table is retrieved from a blackboard using a store key.
/// </summary>
public class MoveToTable : Task
{
    private string _storeKey;

    /// <summary>
    /// Initializes a new instance of the MoveToTable task with the specified store key.
    /// </summary>
    /// <param name="storeKey">The key used to retrieve the Table object from the blackboard.</param>
    public MoveToTable(string storeKey)
    {
        // Check if the store key is null or empty and throw an appropriate exception if it is.
        if (string.IsNullOrEmpty(storeKey))
            throw new ArgumentNullException(nameof(storeKey), "Store key cannot be null or empty.");
        _storeKey = storeKey;
    }

    /// <summary>
    /// Updates the task, attempting to move the agent to the table specified in the blackboard.
    /// </summary>
    /// <param name="agent">The agent performing the task.</param>
    /// <param name="blackboard">The blackboard containing necessary data for task execution.</param>
    /// <exception cref="InvalidOperationException">Thrown if the necessary data is not available in the blackboard.</exception>
    protected override void OnUpdate(Component agent, Blackboard blackboard)
    {
        // Attempt to retrieve the PlayerController from the blackboard.
        if (!blackboard.TryRead("Player Controller", out PlayerController playerController))
        {
            throw new InvalidOperationException("Player Controller is not available in the blackboard.");
        }

        // Attempt to retrieve the Table object from the blackboard using the stored key.
        blackboard.TryRead(_storeKey, out Table table);

        // If the table is null, there's nothing to do. End the action.
        if (table == null)
        {
            EndAction();
            return;
        }

        // Move the player controller to the specified table and then end the action.
        playerController.MoveToTable(table);

        Vector3 tableToAgent = table.transform.position - agent.transform.position;
        var distance = tableToAgent.magnitude;

        if (distance > 1.5f) return;
        EndAction();
    }
}