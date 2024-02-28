using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cooking/Recipe")]
public class Recipe : ScriptableObject
{
    public List<Ingredient> ingredients;
}