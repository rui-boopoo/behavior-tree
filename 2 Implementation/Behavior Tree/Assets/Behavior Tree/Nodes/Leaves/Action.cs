using System.Collections.Generic;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Action : Leaf
    {
        private Task _task;

        public Task task
        {
            get => _task;
            protected set => _task = value;
        }

        public override IReadOnlyList<Node> children => null;

        public Action()
        {
        }

        public Action(Task task)
        {
            AssignTask(task);
        }

        protected override Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            task.Update(agent, blackboard);
            return task.endAction ? Status.Success : Status.Running;
        }

        public void AssignTask(Task task)
        {
            this.task = task ?? throw new System.NullReferenceException(nameof(task));
        }
    }
}