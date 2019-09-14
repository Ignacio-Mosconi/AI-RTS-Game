namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class ConditionalNode : BehaviorNode
    {
        public ConditionalNode()
        {

        }

        protected abstract bool EvaluateCondition();

        public override BehaviorNodeState EvaluateState()
        {
            return (EvaluateCondition()) ? BehaviorNodeState.Success : BehaviorNodeState.Failure; 
        }
    }
}