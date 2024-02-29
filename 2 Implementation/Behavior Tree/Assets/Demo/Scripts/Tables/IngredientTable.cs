using UnityEngine;

public class IngredientTable : Table
{
    [SerializeField] private IngredientHandler _ingredientPrefab;

    public override bool Interact(Component item)
    {
        Debug.LogWarning($"You are trying to place an item on {GetType().Name}, which is impossible");
        return false;
    }

    public override Component Interact(out bool success)
    {
        IngredientHandler ingredient = Instantiate(_ingredientPrefab);
        GameManager.instance.playerHand.Hold(ingredient);
        success = ingredient != null;
        return ingredient;
    }
}