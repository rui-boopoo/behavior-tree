using UnityEngine;

public abstract class Table : MonoBehaviour
{
    [SerializeField] private Transform _placePivot;

    public Transform placePivot => _placePivot;

    public System.Action<ITableInteractable> ItemPlaced;
    public System.Action<ITableInteractable> ItemRemoved;
    public abstract bool Interact(Component item);
    public abstract Component Interact(out bool success);

    /// <summary>
    /// Note: The 'targetContainer' parameter is defined with 'ref' to allow direct modification of its reference within the method. 
    /// Without using 'ref', only a copy of the reference would be modified, leaving the original reference outside the method unchanged. 
    /// </summary>
    /// <typeparam name="TTarget">The type of the target container, constrained to be a subclass of Component.</typeparam>
    /// <param name="componentToPlace">The component to be placed inside the target container.</param>
    /// <param name="targetContainer">A reference to the container where the component will be placed. Must be passed with 'ref'.</param>
    /// <returns>True if the operation is successful, otherwise false.</returns>
    public bool Place<TTarget>(Component componentToPlace, ref TTarget targetContainer) where TTarget : Component
    {
        // 1. No component passes in or target container has something("is full")
        // 2. Component is not vaild to place in the container
        if (componentToPlace == null || targetContainer != null) return false;
        if (componentToPlace is not TTarget validComponent) return false;

        targetContainer = validComponent;
        targetContainer.transform.SetParent(placePivot);

        return true;
    }
}