using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class SelectorNode : FlowControlNode
    {
        public SelectorNode() : base(behaviorName: "Selector")
        {

        }

        public override BehaviorNodeState UpdateState()
        {
            BehaviorNodeState selectorState = BehaviorNodeState.Running;
            BehaviorNodeState activeNodeState = children[activeNodeIndex].UpdateState();

            switch (activeNodeState)
            {
                case BehaviorNodeState.Success:
                    activeNodeIndex = 0;
                    selectorState = BehaviorNodeState.Success;
                    break;
                
                case BehaviorNodeState.Failure:
                    activeNodeIndex++;
                    if (activeNodeIndex == children.Count)
                    {
                        activeNodeIndex = 0;
                        selectorState = BehaviorNodeState.Failure;
                    }
                    break;

                default:
                    break;
            }

            return selectorState;
        }
    }
}