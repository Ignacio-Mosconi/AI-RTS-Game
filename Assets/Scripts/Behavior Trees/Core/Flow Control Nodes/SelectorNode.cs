using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class SelectorNode : FlowControlNode
    {
        public override BehaviorNodeState UpdateState()
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