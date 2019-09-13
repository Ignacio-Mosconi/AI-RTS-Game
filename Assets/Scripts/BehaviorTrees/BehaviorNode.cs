namespace GreenNacho.AI.BehaviorTrees
{
    public enum BehaviorNodeState
    {
        None, Failure, Success, Running
    }

    [System.Serializable]
    public abstract class BehaviorNode
    {
        protected  BehaviorNodeState nodeState;

        public abstract BehaviorNodeState Evaluate();

        #region Properties

        public BehaviorNodeState NodeState
        {
            get { return nodeState; }
        }

        #endregion
    }
}