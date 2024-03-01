using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public sealed class GameManager : MonoBehaviour
{
    #region Field

    private static GameManager _instance;

    [SerializeField] private List<Recipe> _recipes = new();

    [SerializeField] private Hand _playerHand;

    [SerializeField] private List<Table> _tables;

    [SerializeField] private List<Recipe> _orders = new();

    [SerializeField] private List<CookingMethod> _cookingMethods = new();

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

    public static GameManager instance => _instance;

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

    private void Start()
    {
        Test();
    }

    private void Update()
    {
        // Test();
    }

    public void AddOrder()
    {
        if (_recipes.Count > 0)
        {
            Recipe prototype = _recipes[0];
            var recipe = Recipe.CreateInstance(prototype);

            foreach (var ingredient in recipe.ingredients) Debug.Log(ingredient);

            _orders.Add(recipe);
        }
        else Debug.LogWarning("No recipes available to create an order.");
    }

    public void FinishOrder(List<Ingredient> ingredients)
    {
        var ingredientsSet = new HashSet<Ingredient>(ingredients);

        int index = _orders.FindIndex(recipe =>
        {
            var recipeIngredientsSet = new HashSet<Ingredient>(recipe.ingredients);
            bool isMatch = ingredientsSet.SetEquals(recipeIngredientsSet);
            return isMatch;
        });

        if (index < 0) return;
        Recipe orderToFinish = _orders[index];
        _orders.RemoveAt(index);
        OrderFinished?.Invoke(orderToFinish);
    }

    public void FinishOrder(Plate plate)
    {
        FinishOrder(plate.ingredients);
    }

    public CookingMethod FindCookingMethod(string utensilNameToMatch, RawIngredientHandler input)
    {
        if (input == null || string.IsNullOrEmpty(utensilNameToMatch))
            return null;

        Ingredient currentIngredient = input.ingredient;
        if (currentIngredient == null) return null;

        CookingMethod matchingCookingMethod = _cookingMethods.FirstOrDefault(method =>
            method.utensilName.Equals(utensilNameToMatch) &&
            method.ingredientInput.name.Equals(currentIngredient.name));

        return matchingCookingMethod;
    }

    public void Test()
    {
        if (tables.Count <= 0) return;
        // var serveTable = _tables[0] as UtensilTable;
        // if (serveTable == null) return;

        var hasComponent = playerHand.TryGetComponent(out PlayerController playerController);
        if (!hasComponent) return;

        playerController.MoveToTable(_tables[0]);

        // playerHand.GetComponent<NavMeshAgent>().SetDestination(new Vector3(6.5f, 0, -3));
        // Vector3 newDestination = serveTable.transform.position + serveTable.transform.forward * 1.0f;
        // navMeshAgent.SetDestination(newDestination);
        //
        // if (navMeshAgent.pathPending || !(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)) return;
        // Vector3 directionToTarget = (serveTable.transform.position - navMeshAgent.transform.position).normalized;
        // Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        // navMeshAgent.transform.rotation =
        //     Quaternion.Slerp(navMeshAgent.transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    public IEnumerator StartTest()
    {
        if (tables.Count <= 0) yield break;
        var serveTable = _tables[0] as UtensilTable;
        if (serveTable == null) yield break;


        var navMeshAgent = playerHand.gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.Move(serveTable.transform.position - playerHand.transform.position);
    }
}