using UnityEngine;

namespace GreenNacho.AI.BehaviorTrees
{
    public enum BehaviorNodeState
    {
        None, Failure, Success, Running
    }

    [System.Serializable]
    public abstract class BehaviorNode
    {
        protected string behaviorName;

        public BehaviorNode(string behaviorName)
        {
            this.behaviorName = behaviorName;
        }

        public abstract BehaviorNodeState UpdateState();

        public virtual void OnStateUpdate()
        {
            Debug.Log(behaviorName);
        }
    }
}