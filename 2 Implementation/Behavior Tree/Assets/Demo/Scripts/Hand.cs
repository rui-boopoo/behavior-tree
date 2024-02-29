using UnityEngine;

public class Hand : MonoBehaviour
{
    private Component _handHoldable;

    public System.Action<Component> ItemHoldChanged;

    public bool Hold(Component handHoldable)
    {
        if (_handHoldable != null
            || handHoldable == null
            || handHoldable is not IHandHoldable) return false;

        _handHoldable = handHoldable;
        ItemHoldChanged?.Invoke(_handHoldable);

        return true;
    }
}