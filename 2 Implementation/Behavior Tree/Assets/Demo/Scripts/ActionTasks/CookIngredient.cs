using Boopoo.BehaviorTrees;
using UnityEngine;

public class CookIngredient : Task
{
    private string _handlerKey;
    private bool _interacted;

    public CookIngredient(string handlerKey)
    {
        _handlerKey = handlerKey;
    }

    protected override void OnUpdate(Component agent, Blackboard blackboard)
    {
        if (_interacted)
        {
            EndAction();
            return;
        }

        bool hasIngredient = blackboard.TryRead(_handlerKey, out IngredientHandler handler);
        if (!hasIngredient)
            throw new System.NullReferenceException(nameof(hasIngredient));

        var table = blackboard.Read<UtensilTable>("Utensil Table");
        _interacted = table.Interact(handler);
        blackboard.Write("Cooking Finished", table.canGrab);
        EndAction();
    }
}