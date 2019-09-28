using UnityEngine;
using GreenNacho.AI.BehaviorTrees;

namespace MinerBehaviors
{
    [System.Serializable]
    public class DeathCondition<T> : ConditionalNode where T : MonoBehaviour, IDamagable
    {
        T damagable;

        public DeathCondition(T damagable) : base(behaviorName: "Is Dead?")
        {
            this.damagable = damagable;
        }

        protected override bool EvaluateCondition()
        {
            return (damagable.Life <= 0f);
        }
    }
}