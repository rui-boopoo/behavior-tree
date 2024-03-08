using Boopoo.BehaviorTrees;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// A task class designed to find the nearest component of a specified type to the agent and store it in a blackboard.
/// This version is generic and requires the component type to be specified at instantiation.
/// </summary>
/// <typeparam name="T">The type of component to search for, which must derive from Component.</typeparam>
public class FindNearestComponentByType<T> : Task where T : Component
{
    private string _storeKey;
    private SphereCollider _visionSensor;

    /// <summary>
    /// Initializes a new instance of the FindNearestComponentByType class.
    /// </summary>
    /// <param name="storeKey">The key under which to store the found component in the blackboard.</param>
    public FindNearestComponentByType(string storeKey)
    {
        if (string.IsNullOrEmpty(storeKey))
            throw new ArgumentNullException(nameof(storeKey), "Store key cannot be null or empty.");
        _storeKey = storeKey;
    }

    /// <summary>
    /// Finds the nearest component of the specified type to the agent and updates the blackboard.
    /// </summary>
    /// <param name="agent">The agent performing the task.</param>
    /// <param name="blackboard">The blackboard used for storing and retrieving data.</param>
    protected override void OnUpdate(Component agent, Blackboard blackboard)
    {
        // Ensure the vision sensor is available.
        if (_visionSensor == null && !blackboard.TryRead("Vision Sensor", out _visionSensor))
        {
            throw new NullReferenceException(
                $"The {agent.name} does not have a 'Vision Sensor' defined in the blackboard, necessary for component detection.");
        }

        // Retrieve all colliders within range of the agent.
        var collidersInRange = Physics.OverlapSphere(agent.transform.position, _visionSensor.radius);

        // Find the nearest component of type T.
        T nearestComponent = collidersInRange
            .Select(collider => collider.GetComponent<T>())
            .Where(component => component != null)
            .OrderBy(component => (component.transform.position - agent.transform.position).sqrMagnitude)
            .FirstOrDefault();

        // Register the found component to the blackboard, if any.
        blackboard.Write(_storeKey, nearestComponent);

        EndAction();
    }
}