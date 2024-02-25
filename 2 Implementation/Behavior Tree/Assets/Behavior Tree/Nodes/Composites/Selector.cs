using System.Linq;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Selector : Composite
    {
        protected override bool OnEvaluate(Component agent, Blackboard blackboard)
        {
            return children.Any(child => child.Evaluate(agent, blackboard));
        }
    }
}