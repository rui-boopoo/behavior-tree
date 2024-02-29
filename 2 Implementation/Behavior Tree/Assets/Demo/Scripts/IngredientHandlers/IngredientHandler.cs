using UnityEngine;

public abstract class IngredientHandler : MonoBehaviour, IHandHoldable, ITableInteractable
{
    [SerializeField] private Ingredient _ingredient;

    public Ingredient ingredient
    {
        get => _ingredient;
        protected set => _ingredient = value;
    }

    public void Initialize(Ingredient ingredient)
    {
        this.ingredient = ingredient;
    }
}