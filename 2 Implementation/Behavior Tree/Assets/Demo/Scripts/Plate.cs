using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour, IHandHoldable, ITableInteractable
{
    List<Ingredient> _ingredients = new();

    public List<Ingredient> ingredients
    {
        get => _ingredients;
        private set => _ingredients = value;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void AddIngredients(params Ingredient[] ingredientArray)
    {
        ingredients.AddRange(ingredientArray);
    }

    public int CalculateScore()
    {
        throw new System.NotImplementedException();
    }
}