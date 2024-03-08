using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Cooking/Recipe")]
public class Recipe : ScriptableObject
{
    public List<Ingredient> ingredients;

    public static Recipe CreateInstance(Recipe prototype)
    {
        var instance = CreateInstance<Recipe>();
        var fields = typeof(Recipe).GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo fieldInfo in fields)
        {
            if (fieldInfo.FieldType == typeof(List<Ingredient>))
            {
                if (fieldInfo.GetValue(prototype) is not List<Ingredient> originalList) continue;
                
                List<Ingredient> newList = new(originalList.Count);
                newList.AddRange(originalList);
                fieldInfo.SetValue(instance, newList);
            }
            else
            {
                fieldInfo.SetValue(instance, fieldInfo.GetValue(prototype));
            }
        }

        return instance;
    }
}