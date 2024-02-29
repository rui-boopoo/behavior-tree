using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public sealed class GameManager : MonoBehaviour
{
    #region Field

    private GameManager _instance;

    [SerializeField] private List<Recipe> _recipes = new();

    [SerializeField] private Hand _playerHand;

    [SerializeField] private List<Table> _tables;

    [SerializeField] private List<Recipe> _orders = new();

    public System.Action<Recipe> OrderFinished;

    #endregion

    #region Propety

    public List<Recipe> recipes
    {
        get => _recipes;
        private set => _recipes = value;
    }

    public Hand playerHand
    {
        get => _playerHand;
        set => _playerHand = value;
    }

    public List<Table> tables
    {
        get => _tables;
        private set => _tables = value;
    }

    public GameManager instance => _instance;

    #endregion

    [UsedImplicitly]
    private void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Debug.LogWarning($"More than one {GetType().Name} instance in the scene, keeps the first one");
            DestroyImmediate(this);
        }

        _tables = new List<Table>(FindObjectsOfType<Table>());
    }

    public void AddOrder()
    {
        if (_recipes.Count > 0)
        {
            Recipe prototype = _recipes[0];
            var recipe = Recipe.CreateInstance(prototype);
            _orders.Add(recipe);
        }
        else Debug.LogWarning("No recipes available to create an order.");
    }

    public void FinishOrder(Recipe order)
    {
        Recipe orderToFinish = _orders.FirstOrDefault((o) => o == order);
        
        if (orderToFinish == null) return;
        _orders.Remove(orderToFinish);
        OrderFinished?.Invoke(order);
    }
}