namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class ConditionalNode : BehaviorNode
    {
        protected abstract bool EvaluateCondition();

        public override BehaviorNodeState UpdateState()
        {
            return (EvaluateCondition()) ? BehaviorNodeState.Success : BehaviorNodeState.Failure; 
        }
    }
}