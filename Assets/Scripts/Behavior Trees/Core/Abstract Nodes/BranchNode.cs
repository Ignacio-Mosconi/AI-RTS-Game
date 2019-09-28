namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class BranchNode : BehaviorNode
    {
        public BranchNode(string behaviorName) : base(behaviorName)
        {

        }

        public abstract void AddChild(BehaviorNode behaviorNode);
        public abstract void RemoveChild(BehaviorNode behaviorNode = null);
    }
}