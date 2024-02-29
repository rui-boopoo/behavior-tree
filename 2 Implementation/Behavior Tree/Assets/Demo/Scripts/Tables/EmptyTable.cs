using UnityEngine;

public class EmptyTable : Table
{
    [SerializeField] private IngredientHandler _item;

    public IngredientHandler item
    {
        get => _item;
        protected set => _item = value;
    }

    public override bool Interact(Component item)
    {
        return this.item == null && Place(item, _item);
    }

    public override Component Interact(out bool success)
    {
        if (item != null)
        {
            success = true;
            return item;
        }

        success = false;
        return null;
    }
}