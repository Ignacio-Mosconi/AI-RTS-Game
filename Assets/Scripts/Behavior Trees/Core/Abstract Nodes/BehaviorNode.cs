namespace GreenNacho.AI.BehaviorTrees
{
    public enum BehaviorNodeState
    {
        None, Failure, Success, Running
    }

    [System.Serializable]
    public abstract class BehaviorNode
    {
        public string NodeName { get; private set; }

        public abstract BehaviorNodeState EvaluateState();
    }
}