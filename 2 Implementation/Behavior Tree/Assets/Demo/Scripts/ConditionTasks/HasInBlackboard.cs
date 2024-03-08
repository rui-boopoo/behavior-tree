using Boopoo.BehaviorTrees;
using System;
using UnityEngine;

public class HasInBlackboard<T> : Task
{
    private string _key;

    public HasInBlackboard(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key), "Store key cannot be null or empty.");
        _key = key;
    }

    protected override bool OnCheck(Component agent, Blackboard blackboard)
    {
        return blackboard.TryRead(_key, out T _);
    }
}