using Boopoo.BehaviorTrees;

public class MoveToTableBehaviorTree<TTable> : BehaviorTree where TTable : Table
{
    private string _tableName;

    public MoveToTableBehaviorTree(string tableName)
    {
        _tableName = tableName;
    }

    protected override string OnInitialize()
    {
        var sequence = new Sequence();

        var hasInBlackBoard = new HasInBlackboard<TTable>(_tableName);
        var condition = new Condition();
        condition.AssignTask(hasInBlackBoard);
        sequence.AddChild(condition);

        var displayActionName = new DisplayCurrentActionName("Move to Table");
        var action = new Action(displayActionName);
        sequence.AddChild(action);

        var moveToTable = new MoveToTable(_tableName);
        action = new Action(moveToTable);
        sequence.AddChild(action);

        root = sequence;

        return null;
    }
}