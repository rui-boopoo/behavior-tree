using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Success : Node
    {
        protected override Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            return Status.Success;
        }
    }
}