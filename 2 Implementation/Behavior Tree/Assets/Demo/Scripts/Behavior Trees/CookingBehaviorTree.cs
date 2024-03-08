using Boopoo.BehaviorTrees;

public class CookingBehaviorTree : BehaviorTree
{
    protected override string OnInitialize()
    {
        var sequence = new Sequence();

        var hasOrder = new HasInBlackboard<Recipe>("Current Order");
        var condition = new Condition();
        condition.AssignTask(hasOrder);
        sequence.AddChild(condition);

        // var hasInBlackBoard = new HasInBlackboard<IngredientTable>("Ingredient Table");
        // condition = new Condition();
        // condition.AssignTask(hasInBlackBoard);
        // sequence.AddChild(condition);
        //
        // var displayActionName = new DisplayCurrentActionName("Move to Table");
        // action = new Action(displayActionName);
        // sequence.AddChild(action);
        //
        // var moveToTable = new MoveToTable("Ingredient Table");
        // action = new Action(moveToTable);
        // sequence.AddChild(action);

        var subtree = CreateInstance<MoveToTableBehaviorTree<IngredientTable>>
            (agent, blackboard, "Ingredient Table");
        var subtreeNode = new Subtree(subtree);
        sequence.AddChild(subtreeNode);

        var grabIngredientBehaviorTree = CreateInstance<GrabIngredientBehaviorTree<IngredientTable>>(agent, blackboard,
            "Ingredient Table", "Raw Rice Handler");
        sequence.AddChild(new Subtree(grabIngredientBehaviorTree));

        var waitForSeconds = new WaitForSeconds(2);
        var action = new Action(waitForSeconds);
        sequence.AddChild(action);

        var aSubTree = CreateInstance<MoveToTableBehaviorTree<UtensilTable>>(agent, blackboard, "Utensil Table");
        subtreeNode = new Subtree(aSubTree);
        sequence.AddChild(subtreeNode);

        var displayActionName = new DisplayCurrentActionName("Cook Ingredient");
        action = new Action(displayActionName);
        sequence.AddChild(action);

        var cookIngredient = new CookIngredient("Raw Rice Handler");
        action = new Action(cookIngredient);
        sequence.AddChild(action);

        displayActionName = new DisplayCurrentActionName("Wait For Condition");
        action = new Action(displayActionName);
        sequence.AddChild(action);

        var waitForCookingFinished = new WaitForCondition((_, blackboard) =>
        {
            bool hasValue = blackboard.TryRead("Utensil Table", out UtensilTable table);
            if (!hasValue) throw new System.ArgumentNullException(nameof(hasValue));
            return table.canGrab;
        });
        action = new Action(waitForCookingFinished);
        sequence.AddChild(action);

        var grabIngredient = CreateInstance<GrabIngredientBehaviorTree<UtensilTable>>(agent, blackboard,
            "Utensil Table", "Steamed Rice Handler");
        sequence.AddChild(new Subtree(grabIngredient));

        root = sequence;

        return null;
    }
}