namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class InverterNode : DecoratorNode
    {
        public InverterNode(BehaviorNode child) : base(child)
        {

        }

        public override BehaviorNodeState EvaluateState()
        {
            BehaviorNodeState nodeState = BehaviorNodeState.None;

            switch (child.EvaluateState())
            {
                case BehaviorNodeState.Success:
                    nodeState = BehaviorNodeState.Failure;
                    break;

                case BehaviorNodeState.Failure:
                    nodeState = BehaviorNodeState.Success;
                    break;

                case BehaviorNodeState.Running:
                    nodeState = BehaviorNodeState.Running;
                    break;
            }
            
            return nodeState;
        }
    }
}