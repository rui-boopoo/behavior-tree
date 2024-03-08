using JetBrains.Annotations;
using UnityEngine;
using static UnityEditor.Progress;

public class UtensilTable : Table
{
    [SerializeField] private string _utensilName;
    [SerializeField] private bool _requireAgentStay;

    private RawIngredientHandler _inputHandler;
    private PreparedIngredientHandler _outputHandler;

    private bool _isCooking;
    private float _cookingTimer;
    private float _timeToCook;

    public bool canGrab => _outputHandler != null && !_isCooking;

    [UsedImplicitly]
    private void Update()
    {
        if (!_isCooking) return;

        _cookingTimer += Time.deltaTime;

        if (!(_cookingTimer >= _timeToCook)) return;
        _isCooking = false;
        Destroy(_inputHandler.gameObject);
        _outputHandler.gameObject.SetActive(true);
    }

    public override bool Interact(Component item)
    {
        if (item == null || item is not RawIngredientHandler handler) return false;
        if (_inputHandler != null) return false;

        _inputHandler = handler;

        CookingMethod cookingMethod = GameManager.instance.FindCookingMethod(_utensilName, _inputHandler);

        if (cookingMethod == null) return false;
        _outputHandler = Instantiate(cookingMethod.outputPrefab, placePivot);
        _outputHandler.gameObject.SetActive(false);
        _timeToCook = cookingMethod.timeToCook;
        _isCooking = true;
        return true;
    }

    public override Component Interact(out bool success)
    {
        // TODO: Put ingredient into player's hand
        if (canGrab)
        {
            success = true;
            Component outputHandler = _outputHandler;
            Reset();
            return outputHandler;
        }

        success = false;
        return null;
    }

    private void Reset()
    {
        _inputHandler = null;
        _outputHandler = null;
        _isCooking = false;
        _cookingTimer = 0;
        _timeToCook = 0;
    }
}