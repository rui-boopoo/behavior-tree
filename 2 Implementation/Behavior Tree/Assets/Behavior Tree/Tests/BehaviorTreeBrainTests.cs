using NUnit.Framework;
using UnityEngine;

namespace Boopoo.BehaviorTrees.Tests
{
    public class BehaviorTreeBrainTests
    {
        private GameObject _gameObject = new();
        private BehaviorTreeBrain _brain;

        [SetUp]
        public void SetUp()
        {
            _gameObject = new GameObject("Test Object");
            _brain = _gameObject.AddComponent<BehaviorTreeBrain>();
            _gameObject.AddComponent<Blackboard>();

            _brain.Initialize<TestBehaviorTree>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_gameObject != null) Object.DestroyImmediate(_gameObject);
        }

        protected class TestBehaviorTree : BehaviorTree
        {
            protected override string OnInitialize()
            {
                root = new Sequence();
                root.AddChild(new Success());

                var subtree = CreateInstance<TestSubBehaviorTree>(agent, blackboard);
                root.AddChild(new Subtree(subtree));

                return null;
            }
        }

        [HideInInspector]
        protected class TestSubBehaviorTree : BehaviorTree
        {
            protected override string OnInitialize()
            {
                root = new Selector();
                root.AddChild(new Failure());
                root.AddChild(new Success());
                return null;
            }
        }

        [Test]
        public void Initialization_BehaviorTreeAndComponents_NotNull()
        {
            Assert.IsNotNull(_brain, "BehaviorTreeBrain is null after SetUp.");
            Assert.IsNotNull(_brain.behaviorTree, "BehaviorTree is null after SetUp.");
        }

        [Test]
        public void BehaviorTreeExecution_ExecutesCorrectly_ReturnsExpectedOutcome()
        {
            Status status = _brain.behaviorTree.Evaluate(_brain.agent, _brain.blackboard);
            Assert.AreEqual(Status.Success, status, "Behavior tree did not execute as expected.");
        }
    }
}