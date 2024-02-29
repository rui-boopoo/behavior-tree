using JetBrains.Annotations;
using UnityEngine;

public class UtensilTable : Table
{
    [SerializeField] private bool _requireAgentStay;
    private IngredientHandler _inputIngredient;
    private IngredientHandler _outputHandler;

    [UsedImplicitly]
    private void Update()
    {
    }

    public override bool Interact(Component item)
    {
        if (item == null) return false;
        // TODO: Check if ingredient matches
        return Place(item, _inputIngredient);
    }

    public override Component Interact(out bool success)
    {
        if (_outputHandler != null)
        {
            success = true;
            return _outputHandler;
        }

        success = false;
        return null;
    }
}