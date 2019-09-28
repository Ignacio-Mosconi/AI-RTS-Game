using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class FlowControlNode : BranchNode
    {
        protected List<BehaviorNode> children;
        protected int activeNodeIndex;

        public FlowControlNode(string behaviorName) : base(behaviorName)
        {
            children = new List<BehaviorNode>();
            activeNodeIndex = 0;
        } 

        public override void AddChild(BehaviorNode behaviorNode)
        {
            children.Add(behaviorNode);
        }

        public override void RemoveChild(BehaviorNode behaviorNode)
        {
            children.Remove(behaviorNode);
        }
    }
}