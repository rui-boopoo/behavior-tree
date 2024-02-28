using System.Collections.Generic;

namespace Boopoo.BehaviorTrees
{
    public class Leaf : Node
    {
        public override IReadOnlyList<Node> children => null;
    }
}
