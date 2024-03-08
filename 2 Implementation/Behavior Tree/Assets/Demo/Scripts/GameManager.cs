using System.Collections;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    #region Field

    private static GameManager _instance;

    [SerializeField] private List<Recipe> _recipes = new();

    [SerializeField] private Hand _playerHand;

    [SerializeField] private List<Table> _tables;

    [SerializeField] private List<Recipe> _orders = new();

    [SerializeField] private List<CookingMethod> _cookingMethods = new();

    public System.Action<Recipe> OrderFinished;

    #endregion

    #region Propety

    public Hand playerHand
    {
        get => _playerHand;
        set => _playerHand = value;
    }

    public List<Table> tables => _tables;

    public static GameManager instance => _instance;

    public IReadOnlyList<Recipe> orders => _orders;

    #endregion

    [UsedImplicitly]
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Debug.LogWarning($"More than one {GetType().Name} instance in the scene, keeps the first one");
            DestroyImmediate(this);
        }

        _tables = new List<Table>(FindObjectsOfType<Table>());
        AddOrder();
    }


    public void AddOrder()
    {
        if (_recipes.Count > 0)
        {
            Recipe prototype = _recipes[0];
            var recipe = Recipe.CreateInstance(prototype);

            foreach (var ingredient in recipe.ingredients) Debug.Log(ingredient);

            _orders.Add(recipe);
        }
        else Debug.LogWarning("No recipes available to create an order.");
    }

    public void FinishOrder(List<Ingredient> ingredients)
    {
        var ingredientsSet = new HashSet<Ingredient>(ingredients);

        int index = _orders.FindIndex(recipe =>
        {
            var recipeIngredientsSet = new HashSet<Ingredient>(recipe.ingredients);
            bool isMatch = ingredientsSet.SetEquals(recipeIngredientsSet);
            return isMatch;
        });

        if (index < 0) return;
        Recipe orderToFinish = _orders[index];
        _orders.RemoveAt(index);
        OrderFinished?.Invoke(orderToFinish);
    }

    public void FinishOrder(Plate plate)
    {
        FinishOrder(plate.ingredients);
    }

    public CookingMethod FindCookingMethod(string utensilNameToMatch, RawIngredientHandler input)
    {
        if (input == null || string.IsNullOrEmpty(utensilNameToMatch))
            return null;

        Ingredient currentIngredient = input.ingredient;
        if (currentIngredient == null) return null;

        CookingMethod matchingCookingMethod = _cookingMethods.FirstOrDefault(method =>
            method.utensilName.Equals(utensilNameToMatch) &&
            method.ingredientInput.name.Equals(currentIngredient.name));

        return matchingCookingMethod;
    }
}