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
        return this.item == null && Place(item, ref _item);
    }

    public override Component Interact(out bool success)
    {
        if (item != null)
        {
            success = true;
            Component tempItem = item;
            item = null;
            return tempItem;
        }

        success = false;
        return null;
    }
}