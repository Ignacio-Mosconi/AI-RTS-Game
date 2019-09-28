namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public struct Conditions
    {
        public ConditionalNode firstCondition;
        public ConditionalNode secondCondition;
    }

    [System.Serializable]
    public abstract class LogicNode : BehaviorNode
    {
        protected Conditions conditions;

        public LogicNode(Conditions conditions)
        {
            this.conditions = conditions;
        }
    }
}