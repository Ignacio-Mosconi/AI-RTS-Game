namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public abstract class LeafNode : BehaviorNode
    {
        public LeafNode(string behaviorName) : base(behaviorName)
        {

        }
    }
}