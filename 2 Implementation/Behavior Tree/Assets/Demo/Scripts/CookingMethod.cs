using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cooking/CookingMethod", order = 2)]
public class CookingMethod : ScriptableObject
{
    [Tooltip("The name of the utensil used in this cooking method.")]
    public string utensilName;

    [Tooltip("The ingredient required as input for this cooking method.")]
    public Ingredient ingredientInput;

    [Tooltip("The ingredient produced as output of this cooking method.")]
    public Ingredient ingredientOutput;

    public PreparedIngredientHandler outputPrefab;

    // in second
    public float timeToCook = 2.0f;
}