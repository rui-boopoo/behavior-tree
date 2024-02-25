using System.Collections.Generic;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public enum Status
    {
        Running = 0,
        Success,
        Failed
    }

    public abstract class Node
    {
        #region Field

        private Node _parent;
        private List<Node> _children;

        private Status _status;

        #endregion

        #region Propety

        public Node parent
        {
            get => _parent;
            set => _parent = value;
        }

        public List<Node> children
        {
            get => _children;
            set => _children = value;
        }

        public Status status => _status;

        #endregion

        #region Public Interface

        // evaluate flow of each node:
        // update status and evaluate node
        public bool Evaluate(Component agent, Blackboard blackboard)
        {
            _status = Status.Running;
            bool success = OnEvaluate(agent, blackboard);
            _status = success ? Status.Success : Status.Failed;
            return success;
        }

        #endregion

        // evaluate implementation of the node
        // detail: should not call other's OnEvaluate, although it's a protected function
        // the public interface is Evaluate
        protected virtual bool OnEvaluate(Component agent, Blackboard blackboard)
        {
            return false;
        }
    }
}