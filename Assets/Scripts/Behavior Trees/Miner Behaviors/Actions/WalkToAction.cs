using UnityEngine;
using GreenNacho.AI.BehaviorTrees;
using GreenNacho.AI.Pathfinding;

namespace MinerBehaviors
{
    [System.Serializable]
    public class WalkToAction<T> : LeafNode where T : MonoBehaviour, IAgent
    {
        T agent;
        Vector3 targetPosition;

        public WalkToAction(T agent, Vector3 targetPosition, string destinationName) : base(behaviorName: "Walk To" + destinationName)
        {
            this.agent = agent;
            this.targetPosition = targetPosition;
        }

        public override BehaviorNodeState UpdateState()
        {
            BehaviorNodeState actionState = BehaviorNodeState.Running; 

            agent.PathNodeAgent.SimpleMove(targetPosition);

            if (agent.PathNodeAgent.HasReachedDestination)
                actionState = BehaviorNodeState.Success;

            return actionState;
        }
    }
}