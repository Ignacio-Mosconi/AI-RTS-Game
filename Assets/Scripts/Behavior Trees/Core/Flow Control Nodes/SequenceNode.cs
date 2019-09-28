using System.Collections.Generic;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class SequenceNode : FlowControlNode
    {
        public SequenceNode(string sequenceName) : base(behaviorName: sequenceName + " Sequence")
        {

        }

        public override BehaviorNodeState UpdateState()
        {
            base.OnStateUpdate();

            BehaviorNodeState sequenceState = BehaviorNodeState.Running;
            BehaviorNodeState activeNodeState = children[activeNodeIndex].UpdateState();

            switch (activeNodeState)
            {
                case BehaviorNodeState.Success:
                    activeNodeIndex++;
                    if (activeNodeIndex == children.Count)
                    {
                        activeNodeIndex = 0;
                        sequenceState = BehaviorNodeState.Success;
                    }
                    break;

                case BehaviorNodeState.Failure:
                    activeNodeIndex = 0;
                    sequenceState = BehaviorNodeState.Failure;
                    break;

                default:
                    break;
            }

            return sequenceState;
        }
    }
}