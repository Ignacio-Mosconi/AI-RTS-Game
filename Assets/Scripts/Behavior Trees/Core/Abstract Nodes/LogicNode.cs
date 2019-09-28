namespace GreenNacho.AI.BehaviorTrees
{
    public enum LogicOperator
    {
        And, Or, Xor
    }

    [System.Serializable]
    public struct Conditions
    {
        public ConditionalNode firstCondition;
        public ConditionalNode secondCondition;
    }

    [System.Serializable]
    public class LogicNode : BehaviorNode
    {
        LogicOperator logicOperator;
        Conditions conditions;

        public void SetLogicOperator(LogicOperator logicOperator)
        {
            this.logicOperator = logicOperator;
        }

        public void SetFirstCondition(ConditionalNode conditionalNode)
        {
            conditions.firstCondition = conditionalNode;
        }

        public void SetSecondCondition(ConditionalNode conditionalNode)
        {
            conditions.secondCondition = conditionalNode;
        }

        public override BehaviorNodeState UpdateState()
        {
            BehaviorNodeState nodeState = BehaviorNodeState.None;

            bool firstConditionMet = (conditions.firstCondition.UpdateState() == BehaviorNodeState.Success);
            bool secondConditionMet = (conditions.secondCondition.UpdateState() == BehaviorNodeState.Success);

            switch (logicOperator)
            {
                case LogicOperator.And:
                    nodeState = (firstConditionMet && secondConditionMet) ? BehaviorNodeState.Success : BehaviorNodeState.Failure;
                    break;
                
                case LogicOperator.Or:
                    nodeState = (firstConditionMet || secondConditionMet) ? BehaviorNodeState.Success : BehaviorNodeState.Failure;
                    break;
                
                case LogicOperator.Xor:
                    nodeState = (firstConditionMet ^ secondConditionMet) ? BehaviorNodeState.Success : BehaviorNodeState.Failure;
                    break;
            }

            return nodeState;
        }
    }
}