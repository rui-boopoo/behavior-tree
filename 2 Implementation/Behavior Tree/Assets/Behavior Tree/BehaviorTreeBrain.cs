using JetBrains.Annotations;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    [RequireComponent(typeof(Blackboard))]
    public class BehaviorTreeBrain : MonoBehaviour
    {
        private BehaviorTree _behaviorTree;

        private Component _agent;
        private Blackboard _blackboard;

        public BehaviorTree behaviorTree
        {
            get => _behaviorTree;
            protected set => _behaviorTree = value;
        }

        // private Node _root;
        //

        #region Propety

        public Component agent => _agent;

        public Blackboard blackboard => _blackboard;

        #endregion

        #region Unity Callbacks

        [UsedImplicitly]
        private void Awake()
        {
            _agent = this;
            _blackboard = GetComponent<Blackboard>();
        }

        [UsedImplicitly]
        private void Update()
        {
            behaviorTree?.Update();
        }


        public void Initialize<TBehaviorTree>() where TBehaviorTree : BehaviorTree, new()
        {
            var tree = BehaviorTree.CreateInstance<TBehaviorTree>(agent, blackboard);
            behaviorTree = tree;
        }

        #endregion
    }
}