using UnityEngine;

[CreateAssetMenu(menuName = "Cooking/Ingredient")]
public class Ingredient : ScriptableObject
{
    public new string name = string.Empty;
    public Mesh mesh;

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        var other = (Ingredient)obj;
        return string.Equals(name, other.name);
    }

    public override int GetHashCode()
    {
        return name?.GetHashCode() ?? 0;
    }
}