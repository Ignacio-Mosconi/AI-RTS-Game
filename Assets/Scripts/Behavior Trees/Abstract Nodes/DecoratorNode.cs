namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class DecoratorNode : BehaviorNode
    {
        protected BehaviorNode child;

        public DecoratorNode(BehaviorNode child)
        {
            this.child = child;
        }
    }
}