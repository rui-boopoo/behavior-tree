using System.Linq;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Sequence : Composite
    {
        protected override bool OnEvaluate(Component agent, Blackboard blackboard)
        {
            return children.All(child => child.Evaluate(agent, blackboard));
        }
    }
}