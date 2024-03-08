using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public abstract class Task
    {
        private bool _endAction = false;

        public bool endAction => _endAction;

        public void Update(Component agent, Blackboard blackboard)
        {
            OnUpdate(agent, blackboard);
        }

        public bool CheckCondition(Component agent, Blackboard blackboard)
        {
            return OnCheck(agent, blackboard);
        }

        protected virtual bool OnCheck(Component agent, Blackboard blackboard)
        {
            return false;
        }

        protected virtual void OnUpdate(Component agent, Blackboard blackboard)
        {
        }

        public void EndAction(bool value)
        {
            _endAction = value;
        }

        public void EndAction()
        {
            EndAction(true);
        }
    }
}