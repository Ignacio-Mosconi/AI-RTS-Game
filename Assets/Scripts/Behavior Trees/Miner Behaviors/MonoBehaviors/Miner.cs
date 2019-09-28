using UnityEngine;

namespace MinerBehaviors
{
    public class Miner : MonoBehaviour, IDamagable
    {
        float life;

        public bool IsDead()
        {
            return (life <= 0f);
        }
    }
}