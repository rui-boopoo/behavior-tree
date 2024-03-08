using Boopoo.BehaviorTrees;
using UnityEngine;

public class CookingBehaviorTree : BehaviorTree
{
    protected override string OnInitialize()
    {
        var sequence = new Sequence();
        var selector = new Selector();
        var action = new Action();
        var condition = new Condition();

        var hasOrder = new HasInBlackboard<Recipe>("Current Order");
        condition = new Condition();
        condition.AssignTask(hasOrder);
        sequence.AddChild(condition);

        var hasInBlackBoard = new HasInBlackboard<IngredientTable>("Ingredient Table");
        condition = new Condition();
        condition.AssignTask(hasInBlackBoard);
        sequence.AddChild(condition);

        var displayActionName = new DisplayCurrentActionName("Move to Table");
        action = new Action(displayActionName);
        sequence.AddChild(action);

        var moveToTable = new MoveToTable("Ingredient Table");
        action = new Action(moveToTable);
        sequence.AddChild(action);

        displayActionName = new DisplayCurrentActionName("Grab Ingredient");
        action = new Action(displayActionName);
        sequence.AddChild(action);

        var grabIngredient = new GrabIngredient<IngredientTable>("Ingredient Table", "Raw Rice Handler");
        action = new Action(grabIngredient);
        sequence.AddChild(action);

        var waitForSeconds = new WaitForSeconds(2);
        action = new Action(waitForSeconds);
        sequence.AddChild(action);

        var hasUtensilTable = new HasInBlackboard<UtensilTable>("Utensil Table");
        condition = new Condition();
        condition.AssignTask(hasUtensilTable);
        sequence.AddChild(condition);

        displayActionName = new DisplayCurrentActionName("Move to Table");
        action = new Action(displayActionName);
        sequence.AddChild(action);

        moveToTable = new MoveToTable("Utensil Table");
        action = new Action(moveToTable);
        sequence.AddChild(action);

        displayActionName = new DisplayCurrentActionName("Cook Ingredient");
        action = new Action(displayActionName);
        sequence.AddChild(action);

        var cookIngredient = new CookIngredient("Raw Rice Handler");
        action = new Action(cookIngredient);
        sequence.AddChild(action);

        displayActionName = new DisplayCurrentActionName("Wait...");
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

        displayActionName = new DisplayCurrentActionName("Grab Ingredient");
        action = new Action(displayActionName);
        sequence.AddChild(action);

        var grabSteamedRice = new GrabIngredient<UtensilTable>("Utensil Table", "Steamed Rice Handler");
        action = new Action(grabSteamedRice);
        sequence.AddChild(action);

        root = sequence;

        return null;
    }
}