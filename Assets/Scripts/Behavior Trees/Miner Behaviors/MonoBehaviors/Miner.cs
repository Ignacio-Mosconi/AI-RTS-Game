using UnityEngine;

namespace MinerBehaviors
{
    public class Miner : MonoBehaviour, IDamagable, IResourceCarrier
    {
        [SerializeField] Resource resourceCarried = Resource.Gold;
        [SerializeField, Range(0f, 100f)] float life;
        [SerializeField, Range(0, 100)] int maxCarryAmount;

        int amountCarried;

        public Resource ResourceCarried
        {
            get { return resourceCarried;}
        }

        public float Life
        {
            get { return life; }
        }

        public int AmountCarried
        {
            get { return amountCarried; }
        }

        public int MaxCarryAmount
        {
            get { return maxCarryAmount; }
        }
    }
}