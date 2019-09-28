using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class SequenceNode : FlowControlNode
    {
        public SequenceNode(List<BehaviorNode> children) : base(children)
        {
            
        }

        public override BehaviorNodeState EvaluateState()
        {
            BehaviorNodeState nodeState = children[activeNodeIndex].EvaluateState();

            switch (nodeState)
            {
                case BehaviorNodeState.Success:
                    activeNodeIndex++;
                    if (activeNodeIndex == children.Count)
                        activeNodeIndex = 0;
                    break;

                case BehaviorNodeState.Failure:
                    activeNodeIndex = 0;
                    break;

                default:
                    break;
            }

            return nodeState;
        }
    }
}