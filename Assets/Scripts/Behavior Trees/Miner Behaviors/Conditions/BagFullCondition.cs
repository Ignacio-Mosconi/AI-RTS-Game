using UnityEngine;
using GreenNacho.AI.BehaviorTrees;

namespace MinerBehaviors
{
    [System.Serializable]
    public class BagFullCondition<T> : ConditionalNode where T : MonoBehaviour, IResourceCarrier
    {
        T resourceCarrier;

        public BagFullCondition(T resourceCarrier) : base(behaviorName: "Is Bag Full?")
        {
            this.resourceCarrier = resourceCarrier;
        }

        protected override bool EvaluateCondition()
        {
            return (resourceCarrier.AmountCarried == resourceCarrier.MaxCarryAmount);
        }
    }
}