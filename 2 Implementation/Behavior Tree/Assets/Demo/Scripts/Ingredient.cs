using UnityEngine;

[CreateAssetMenu(menuName = "Cooking/Ingredient")]
public class Ingredient : ScriptableObject
{
    public new string name = string.Empty;
    public Mesh mesh;
}