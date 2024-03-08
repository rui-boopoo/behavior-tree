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
        sequence.AddChild(new Action(displayActionName));

        var showGoal = new ShowGoal(_tableName);
        sequence.AddChild(new Action(showGoal));

        var moveToTable = new MoveToTable(_tableName);
        sequence.AddChild(new Action(moveToTable));

        showGoal = new ShowGoal(_tableName, false);
        sequence.AddChild(new Action(showGoal));
        
        var waitForSeconds = new WaitForSeconds(1.0f);
        sequence.AddChild(new Action(waitForSeconds));

        root = sequence;

        return null;
    }
}