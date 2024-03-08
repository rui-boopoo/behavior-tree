using Boopoo.BehaviorTrees;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CookingBehaviorTreeBrain : BehaviorTreeBrain
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private Hand _hand;
    [SerializeField] private TextMeshPro _currentActionTextRef;

    protected override void OnAwake()
    {
        Initialize<CookingBehaviorTree>();
        InitializeWorkingMemory();
    }

    private void InitializeWorkingMemory()
    {
        blackboard.Write("Player Controller", _playerController);
        blackboard.Write("Hand", _hand);

        foreach (Table table in GameManager.instance.tables)
            blackboard.Write($"{ObjectNames.NicifyVariableName(table.GetType().Name)}", table);

        blackboard.Write("Current Order", GameManager.instance.orders[0]);

        blackboard.Write("Current Action Text", _currentActionTextRef);

        blackboard.Print();
    }
}