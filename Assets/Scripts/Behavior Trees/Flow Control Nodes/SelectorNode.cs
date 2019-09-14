using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class SelectorNode : FlowControlNode
    {
        public SelectorNode(List<BehaviorNode> children) : base(children)
        {

        }

        public override BehaviorNodeState EvaluateState()
        {
            BehaviorNodeState nodeState = BehaviorNodeState.None;

            foreach (BehaviorNode behaviorNode in children)
            {
                switch (behaviorNode.EvaluateState())
                {
                    case BehaviorNodeState.Failure:
                        break;
                    
                    case BehaviorNodeState.Success:
                        nodeState = BehaviorNodeState.Success;
                        break;

                    case BehaviorNodeState.Running:
                        nodeState = BehaviorNodeState.Running;
                        break;
                    default:
                        break;
                }

                if (nodeState != BehaviorNodeState.None)
                    break;
            }

            return nodeState;
        }
    }
}