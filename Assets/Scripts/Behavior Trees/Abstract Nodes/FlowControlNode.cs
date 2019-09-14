using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class FlowControlNode : BehaviorNode
    {
        protected List<BehaviorNode> children;

        public FlowControlNode(List<BehaviorNode> children)
        {
            this.children = children;
        } 
    }
}