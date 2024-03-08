using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Success : Leaf
    {
        protected override Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            return Status.Success;
        }
    }
}