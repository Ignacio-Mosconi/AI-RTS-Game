using UnityEngine;
using GreenNacho.AI.BehaviorTrees;

namespace MinerBehaviors
{
    [System.Serializable]
    public class DepositAction<T> : LeafNode where T : MonoBehaviour, IResourceCarrier
    {
        T resourceCarrier;
        float depositDuration;
        float depositTimer;

        public DepositAction(T resourceCarrier, float depositDuration) : base(behaviorName: "Deposit")
        {
            this.resourceCarrier = resourceCarrier;
            this.depositDuration = depositDuration;
            depositTimer = 0f;
        }

        public override BehaviorNodeState UpdateState()
        {
            BehaviorNodeState actionState = BehaviorNodeState.Running;

            depositTimer += Time.deltaTime;

            if (depositTimer >= depositDuration)
            {
                depositTimer = 0f;
                actionState = BehaviorNodeState.Success;
            }

            return actionState;
        }
    }
}