using UnityEngine;

public abstract class Table : MonoBehaviour
{
    [SerializeField] private Transform _placePivot;

    public Transform placePivot => _placePivot;

    public System.Action<ITableInteractable> ItemPlaced;
    public System.Action<ITableInteractable> ItemRemoved;
    public abstract bool Interact(Component item);
    public abstract Component Interact(out bool success);

    public bool Place<TTarget>(Component componentToPlace, TTarget targetContainer) where TTarget : Component
    {
        if (componentToPlace == null || targetContainer == null) return false;
        if (componentToPlace is not TTarget validComponent) return false;

        targetContainer = validComponent;
        targetContainer.transform.SetParent(placePivot);

        return true;
    }
}