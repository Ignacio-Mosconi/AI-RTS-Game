namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class XorNode : LogicNode
    {
        public XorNode(Conditions conditions) : base(conditions)
        {
            
        }

        public override BehaviorNodeState EvaluateState()
        {
            BehaviorNodeState nodeState = BehaviorNodeState.None;

            bool firstConditionMet = (conditions.firstCondition.EvaluateState() == BehaviorNodeState.Success);
            bool secondConditionMet = (conditions.secondCondition.EvaluateState() == BehaviorNodeState.Success);
            
            nodeState = ((firstConditionMet && secondConditionMet) || 
                        (firstConditionMet && !secondConditionMet) ||
                        (!firstConditionMet && secondConditionMet)) ? BehaviorNodeState.Success : BehaviorNodeState.Failure;

            return nodeState;
        }
    }
}