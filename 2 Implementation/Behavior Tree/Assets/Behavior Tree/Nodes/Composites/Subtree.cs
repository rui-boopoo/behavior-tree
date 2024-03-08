using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Subtree : Composite
    {
        private readonly BehaviorTree _tree;

        /// <summary>
        /// Initializes a new instance of the Subtree class with a given behavior tree.
        /// </summary>
        /// <param name="brain">The behavior tree to be executed by this subtree.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="brain"/> is null.</exception>
        public Subtree(BehaviorTree tree)
        {
            _tree = tree ?? throw new System.ArgumentNullException(nameof(tree),
                "Subtree cannot be initialized with a null behavior tree.");
        }

        /// <summary>
        /// Evaluates the subtree within the context of the provided agent and blackboard.
        /// </summary>
        /// <param name="agent">The agent executing the subtree.</param>
        /// <param name="blackboard">The blackboard containing relevant data for the behavior tree's execution.</param>
        /// <returns>The status of the subtree's evaluation.</returns>
        protected override Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            return _tree.Evaluate(agent, blackboard);
        }
    }
}