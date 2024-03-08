using Boopoo.BehaviorTrees;
using TMPro;
using UnityEngine;

public class CookingBehaviorTreeBrain : BehaviorTreeBrain
{
    [SerializeField] private PlayerController _playerController = null;

    [SerializeField] private Hand _hand = null;
    [SerializeField] private TextMeshPro _currentActionTextRef = null;

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
            blackboard.Write($"{NicifyVariableName(table.GetType().Name)}", table);

        blackboard.Write("Current Order", GameManager.instance.orders[0]);

        blackboard.Write("Current Action Text", _currentActionTextRef);

        blackboard.Print();
    }

    public static string NicifyVariableName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return name;
        }

        var niceName = "";
        var wasLower = char.IsLower(name[0]);

        for (int i = 0; i < name.Length; i++)
        {
            if (name[i] == '_' || name[i] == '-')
            {
                niceName += " ";
                wasLower = false;
            }
            else if (char.IsUpper(name[i]))
            {
                if (i > 0 && wasLower)
                {
                    niceName += " ";
                }

                niceName += name[i];
                wasLower = false;
            }
            else
            {
                niceName += i == 0 ? char.ToUpper(name[i]) : name[i];
                wasLower = true;
            }
        }

        return niceName;
    }
}