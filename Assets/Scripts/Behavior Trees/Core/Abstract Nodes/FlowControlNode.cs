using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class FlowControlNode : BehaviorNode
    {
        protected List<BehaviorNode> children;
        protected int activeNodeIndex;

        public FlowControlNode(List<BehaviorNode> children)
        {
            if (children != null)
                this.children = children;
            activeNodeIndex = 0;
        } 
    }
}