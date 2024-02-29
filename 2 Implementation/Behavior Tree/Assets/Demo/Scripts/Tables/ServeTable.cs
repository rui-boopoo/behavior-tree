using UnityEngine;

public class ServeTable : Table
{
    [SerializeField] private Plate _input;

    public override bool Interact(Component item)
    {
        if (item == null || item is not Plate plate) return false;
        GameManager.instance.FinishOrder(plate);
        return true;
    }

    public override Component Interact(out bool success)
    {
        Debug.LogWarning($"You are trying to grab something from {GetType().Name} which is not possible.");
        success = false;
        return null;
    }
}