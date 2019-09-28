using UnityEngine;
using GreenNacho.AI.BehaviorTrees;

namespace MinerBehaviors
{
    [System.Serializable]
    public class MineAction<T> : LeafNode where T : MonoBehaviour, IResourceCarrier
    {
        T resourceCarrier;
        float mineIntervals;
        float mineTimer;
        int resourceMinedPerInterval;

        public MineAction(T resourceCarrier, float mineIntervals, int resourceMinedPerInterval) : base(behaviorName: "Mine")
        {
            this.resourceCarrier = resourceCarrier;
            this.mineIntervals = mineIntervals;
            this.resourceMinedPerInterval = resourceMinedPerInterval;
            mineTimer = 0f;
        }

        public override BehaviorNodeState UpdateState()
        {
            base.OnStateUpdate();

            BehaviorNodeState actionState = BehaviorNodeState.Running;

            mineTimer += Time.deltaTime;

            if (mineTimer >= mineIntervals)
            {
                mineTimer = 0f;
                resourceCarrier.AmountCarried += resourceMinedPerInterval;

                if (resourceCarrier.AmountCarried >= resourceCarrier.MaxCarryAmount)
                {
                    resourceCarrier.AmountCarried = resourceCarrier.MaxCarryAmount;
                    actionState = BehaviorNodeState.Success;
                }
            }

            return actionState;
        }
    }
}