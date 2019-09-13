namespace GreenNacho.AI.BehaviorTrees
{
    public class InverterNode : DecoratorNode
    {
        public InverterNode(BehaviorNode child) : base(child)
        {

        }

        public override BehaviorNodeState Evaluate()
        {
            BehaviorNodeState nodeState = BehaviorNodeState.None;

            switch (child.Evaluate())
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