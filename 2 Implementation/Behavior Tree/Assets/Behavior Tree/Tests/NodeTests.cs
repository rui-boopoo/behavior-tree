using NUnit.Framework;

namespace Boopoo.BehaviorTrees.Tests
{
    public class NodeTests
    {
        // Sequence

        [Test]
        public void Sequence_AllChildrenSuccess_ReturnsSuccess()
        {
            var sequence = new Sequence();
            sequence.AddChild(new Success());
            sequence.AddChild(new Success());

            Status result = sequence.Evaluate(null, null);
            Assert.AreEqual(Status.Success, result);
        }

        [Test]
        public void Sequence_OneChildFails_ReturnsFailure()
        {
            var sequence = new Sequence();
            sequence.AddChild(new Success());
            sequence.AddChild(new Failure());
            
            Status result = sequence.Evaluate(null, null);

            Assert.AreEqual(Status.Failure, result);
        }

        [Test]
        public void Sequence_NoChildren_ReturnsSuccess()
        {
            var sequence = new Sequence();
            
            Status result = sequence.Evaluate(null, null);
            Assert.AreEqual(Status.Success, result);
        }

        // Selector

        [Test]
        public void Selector_OneChildSuccess_ReturnsSuccess()
        {
            var selector = new Selector();
            selector.AddChild(new Failure());
            selector.AddChild(new Success());
            
            Status result = selector.Evaluate(null, null);

            Assert.AreEqual(Status.Success, result);
        }

        [Test]
        public void Selector_AllChildrenFail_ReturnsFailure()
        {
            var selector = new Selector();
            selector.AddChild(new Failure());
            selector.AddChild(new Failure());
            
            Status result = selector.Evaluate(null, null);

            Assert.AreEqual(Status.Failure, result);
        }

        [Test]
        public void Selector_NoChildren_ReturnsFailure()
        {
            var selector = new Selector();
            
            Status result = selector.Evaluate(null, null);

            Assert.AreEqual(Status.Failure, result);
        }
    }
}