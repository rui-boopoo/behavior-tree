using Boopoo.BehaviorTrees;
using UnityEngine;

public class ShowGoal : Task
{
    private string _tableName;
    private bool _showGoal;

    public ShowGoal(string tableName, bool showGoal = true)
    {
        _tableName = tableName;
        _showGoal = showGoal;
    }

    protected override void OnUpdate(Component agent, Blackboard blackboard)
    {
        blackboard.TryRead("Goal Indicator", out GoalIndicatorManager indicator);
        blackboard.TryRead(_tableName, out Table table);
        
        if (_showGoal) indicator.ShowAt(table);
        else indicator.Hide();

        EndAction();
    }
}