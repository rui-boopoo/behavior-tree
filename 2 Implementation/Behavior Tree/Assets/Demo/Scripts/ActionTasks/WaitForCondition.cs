using Boopoo.BehaviorTrees;
using System;
using UnityEngine;

public class WaitForCondition : Task
{
    private bool _pass = false;

    Func<Component, Blackboard, bool> _condition;

    public WaitForCondition(Func<Component, Blackboard, bool> condition)
    {
        _condition = condition;
    }

    protected override void OnUpdate(Component agent, Blackboard blackboard)
    {
        _pass = _condition.Invoke(agent, blackboard);
        if (_pass) EndAction();
    }
}