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

            switch (nodeState)
            {
                case BehaviorNodeState.Success:
                    activeNodeIndex = 0;
                    break;
                
                case BehaviorNodeState.Failure:
                    activeNodeIndex++;
                    if (activeNodeIndex == children.Count)
                        activeNodeIndex = 0;
                    break;


                default:
                    break;
            }

            return nodeState;
        }
    }
}