using System;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Sequence : Composite
    {
        protected override Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            foreach (Node child in children)
            {
                Status result = child.Evaluate(agent, blackboard);
                switch (result)
                {
                    case Status.Failure:
                        return Status.Failure;
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        break;
                    case Status.Resting:
                        break;
                    case Status.Error:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return Status.Success;
        }
    }
}