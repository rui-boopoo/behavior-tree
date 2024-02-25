using System.Collections.Generic;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Failure : Node
    {
        public override IReadOnlyList<Node> children => null;

        protected override Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            return Status.Failure;
        }
    }
}