namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class InverterNode : DecoratorNode
    {
        public InverterNode() : base(behaviorName: "Inverter")
        {

        }

        public override BehaviorNodeState UpdateState()
        {
            base.OnStateUpdate();

            BehaviorNodeState nodeState = BehaviorNodeState.None;

            switch (child.UpdateState())
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