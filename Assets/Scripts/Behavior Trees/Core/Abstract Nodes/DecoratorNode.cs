using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class DecoratorNode : BranchNode
    {
        protected BehaviorNode child;

        public DecoratorNode(string behaviorName) : base(behaviorName)
        {

        }

        public override void AddChild(BehaviorNode behaviorNode)
        {
            if (behaviorNode != null)
                child = behaviorNode;
        }

        public override void RemoveChild(BehaviorNode behaviorNode = null)
        {
            child = null;
        }
    }
}