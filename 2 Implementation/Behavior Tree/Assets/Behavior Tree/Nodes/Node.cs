using System.Collections.Generic;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public enum Status
    {
        Failure = 0,
        Success = 1,
        Running = 2,
        Resting = 3,
        Error = 4,
    }

    public abstract class Node
    {
        #region Field

        private Node _parent;
        private List<Node> _children = new();
        private Status _status = Status.Resting;

        #endregion

        #region Propety

        public Node parent
        {
            get => _parent;
            set => _parent = value;
        }

        public virtual IReadOnlyList<Node> children => _children;

        public Status status => _status;

        #endregion

        #region Public Interface

        // evaluate flow of each node:
        // update status and evaluate node
        public Status Evaluate(Component agent, Blackboard blackboard)
        {
            _status = Status.Running;
            return OnEvaluate(agent, blackboard);
        }

        public void AddChild(Node node)
        {
            if (node == null || _children.Contains(node)) return;
            _children.Add(node);
            node.parent = this;
        }

        #endregion

        // evaluate implementation of the node
        // detail: should not call other's OnEvaluate, although it's a protected function
        // the public interface is Evaluate
        protected virtual Status OnEvaluate(Component agent, Blackboard blackboard)
        {
            return Status.Failure;
        }
    }
}