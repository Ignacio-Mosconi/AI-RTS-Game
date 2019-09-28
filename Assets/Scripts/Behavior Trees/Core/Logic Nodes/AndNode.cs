namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class AndNode : LogicNode
    {
        public AndNode(Conditions conditions) : base(conditions)
        {
            
        }

        public override BehaviorNodeState EvaluateState()
        {
            BehaviorNodeState nodeState = BehaviorNodeState.None;

            bool firstConditionMet = (conditions.firstCondition.EvaluateState() == BehaviorNodeState.Success);
            bool secondConditionMet = (conditions.secondCondition.EvaluateState() == BehaviorNodeState.Success);
            
            nodeState = (firstConditionMet && secondConditionMet) ? BehaviorNodeState.Success : BehaviorNodeState.Failure;

            return nodeState;
        }
    }
}