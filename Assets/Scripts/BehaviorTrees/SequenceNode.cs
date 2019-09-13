using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    public class SequenceNode : FlowControlNode
    {
        public SequenceNode(List<BehaviorNode> children) : base(children)
        {
            
        }

        public override BehaviorNodeState Evaluate()
        {
            BehaviorNodeState nodeState = BehaviorNodeState.None;
            bool anyChildRunning = false;

            foreach (BehaviorNode behaviorNode in children)
            {
                switch (behaviorNode.Evaluate())
                {
                    case BehaviorNodeState.Failure:
                        nodeState = BehaviorNodeState.Failure;
                        break;
                    
                    case BehaviorNodeState.Success:
                        break;

                    case BehaviorNodeState.Running:
                        anyChildRunning = true;
                        break;
                    default:
                        break;
                }

                if (nodeState != BehaviorNodeState.None)
                    break;
            }

            if (nodeState == BehaviorNodeState.None)
                nodeState = (anyChildRunning) ? BehaviorNodeState.Running : BehaviorNodeState.Success;

            return nodeState;
        }
    }
}