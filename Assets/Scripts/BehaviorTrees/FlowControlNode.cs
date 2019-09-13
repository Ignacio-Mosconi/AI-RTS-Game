using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    public abstract class FlowControlNode : BehaviorNode
    {
        protected List<BehaviorNode> children;

        public FlowControlNode(List<BehaviorNode> children)
        {
            this.children = children;
        } 
    }
}