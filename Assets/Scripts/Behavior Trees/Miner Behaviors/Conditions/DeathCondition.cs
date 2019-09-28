using UnityEngine;
using GreenNacho.AI.BehaviorTrees;

namespace MinerBehaviors
{
    [System.Serializable]
    public class DeathCondition<T> : ConditionalNode where T : MonoBehaviour, IDamagable
    {
        T damagableEntity;

        public DeathCondition(T damagableEntity)
        {
            this.damagableEntity = damagableEntity;
        }

        protected override bool EvaluateCondition()
        {
            return damagableEntity.IsDead();
        }
    }
}