using Boopoo.BehaviorTrees;
using TMPro;
using UnityEngine;

public class DisplayCurrentActionName : Task
{
    private string _actionName;

    public DisplayCurrentActionName(string actionName)
    {
        _actionName = actionName;
    }
    
    protected override void OnUpdate(Component agent, Blackboard blackboard)
    {
        bool hasTextRef = blackboard.TryRead("Current Action Text", out TextMeshPro textRef);
        if (!hasTextRef) throw new System.NullReferenceException();
        textRef.text = $"<size=12>Behavior tree\n<size=20>{_actionName}";
        EndAction();
    }
}