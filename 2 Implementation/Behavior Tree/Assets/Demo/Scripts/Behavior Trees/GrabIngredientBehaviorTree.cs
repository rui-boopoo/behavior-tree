using Boopoo.BehaviorTrees;

public class GrabIngredientBehaviorTree<TTable> : BehaviorTree where TTable : Table
{
    private string _tableKey;
    private string _itemKey;

    public GrabIngredientBehaviorTree(string tableKey, string itemKey)
    {
        _tableKey = tableKey;
        _itemKey = itemKey;
    }

    protected override string OnInitialize()
    {
        var sequence = new Sequence();

        var displayActionName = new DisplayCurrentActionName("Grab Ingredient");
        sequence.AddChild(new Action(displayActionName));

        var grabIngredient = new GrabIngredient<TTable>(_tableKey, _itemKey);
        sequence.AddChild(new Action(grabIngredient));

        var waitForSeconds = new WaitForSeconds(1f);
        sequence.AddChild(new Action(waitForSeconds));

        displayActionName = new DisplayCurrentActionName("Grab Ingredient :)");
        sequence.AddChild(new Action(displayActionName));

        waitForSeconds = new WaitForSeconds(1.5f);
        sequence.AddChild(new Action(waitForSeconds));

        root = sequence;
        return null;
    }
}