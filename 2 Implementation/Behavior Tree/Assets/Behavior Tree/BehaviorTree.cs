using System;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class BehaviorTree
    {
        private Node _root;

        private Component _agent;
        private Blackboard _blackboard;

        #region Propety

        public Node root
        {
            get => _root;
            protected set => _root = value;
        }

        public Component agent
        {
            get => _agent;
            protected set => _agent = value;
        }

        public Blackboard blackboard
        {
            get => _blackboard;
            protected set => _blackboard = value;
        }

        #endregion

        public void Update()
        {
            Evaluate(agent, blackboard);
        }

        public Status Evaluate(Component agent, Blackboard blackboard)
        {
            return _root.Evaluate(agent, blackboard);
        }

        private string Initialize()
        {
            return OnInitialize();
        }

        // implementation of Initialize,
        // set up root node and construct graph codely in this function
        // return is the error message, if all good return null
        protected virtual string OnInitialize()
        {
            return null;
        }

        public static TBehaviorTree CreateInstance<TBehaviorTree>(Component agent, Blackboard blackboard)
            where TBehaviorTree : BehaviorTree, new()
        {
            var tree = (TBehaviorTree)Activator.CreateInstance(typeof(TBehaviorTree));
            tree.agent = agent;
            tree.blackboard = blackboard;

            string errorMessage = tree.Initialize();
            bool initializationSucceed = errorMessage == null;
            if (!initializationSucceed) throw new Exception(errorMessage);

            return tree;
        }
    }
}