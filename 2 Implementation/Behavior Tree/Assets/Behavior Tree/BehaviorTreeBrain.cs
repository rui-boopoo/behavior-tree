using JetBrains.Annotations;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    [RequireComponent(typeof(Blackboard))]
    public class BehaviorTreeBrain : MonoBehaviour
    {
        private Node _root = null;

        private Component _agent;
        private Blackboard _blackboard;

        #region Propety

        public Component agent => _agent;

        public Blackboard blackboard => _blackboard;

        #endregion

        #region Unity Callbacks

        [UsedImplicitly]
        private void Awake()
        {
            string errorMessage = Initialize();
            bool initializationSucceed = errorMessage == null;
            if (!initializationSucceed) throw new System.Exception(errorMessage);

            _agent = this;
            _blackboard = GetComponent<Blackboard>();
        }

        [UsedImplicitly]
        private void Update()
        {
            Evaluate(agent, blackboard);
        }

        #endregion

        private void Evaluate(Component agent, Blackboard blackboard)
        {
            _root.Evaluate(agent, blackboard);
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
    }
}