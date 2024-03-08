using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Failure : Leaf
    {
        protected override Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            return Status.Failure;
        }
    }
}