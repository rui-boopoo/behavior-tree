using System;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Selector : Composite
    {
        protected override Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            foreach (Node child in children)
            {
                Status result = child.Evaluate(agent, blackboard);
                switch (result)
                {
                    case Status.Success:
                        return Status.Success;
                    case Status.Running:
                        return Status.Running; // Handle running status if behavior trees support it
                    case Status.Failure:
                        break;
                    case Status.Resting:
                        break;
                    case Status.Error:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return Status.Failure;
        }
    }
}