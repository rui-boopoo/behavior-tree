using System.Collections.Generic;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Condition : Leaf
    {
        private Task _task;

        public Task task
        {
            get => _task;
            protected set => _task = value;
        }

        public override IReadOnlyList<Node> children => null;

        protected override Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            task.Update(agent, blackboard);
            bool pass = task.CheckCondition(agent, blackboard);
            return pass ? Status.Success : Status.Failure;
        }

        public void AssignTask(Task task)
        {
            this.task = task ?? throw new System.NullReferenceException(nameof(task));
        }
    }
}