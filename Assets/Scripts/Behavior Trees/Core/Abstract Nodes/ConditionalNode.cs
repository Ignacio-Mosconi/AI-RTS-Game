namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class ConditionalNode : LeafNode
    {
        public ConditionalNode(string behaviorName) : base(behaviorName)
        {

        }

        protected abstract bool EvaluateCondition();

        public override BehaviorNodeState UpdateState()
        {
            return (EvaluateCondition()) ? BehaviorNodeState.Success : BehaviorNodeState.Failure; 
        }
    }
}