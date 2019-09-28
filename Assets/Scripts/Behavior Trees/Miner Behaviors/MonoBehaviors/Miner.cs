using UnityEngine;
using GreenNacho.AI.BehaviorTrees;
using GreenNacho.AI.Pathfinding;

namespace MinerBehaviors
{
    [RequireComponent(typeof(PathNodeAgent))]
    public class Miner : MonoBehaviour, IAgent, IDamagable, IResourceCarrier
    {
        [SerializeField] Resource resourceCarried = Resource.Gold;
        [SerializeField] Transform targetMiningPoint = default;
        [SerializeField] Transform targetDeployPoint = default;
        [SerializeField, Range(0f, 100f)] float life;
        [SerializeField, Range(1f, 5f)] float mineIntervals;
        [SerializeField, Range(1f, 5f)] float depositDuration;
        [SerializeField, Range(0, 100)] int maxCarryAmount;
        [SerializeField, Range(5, 10)] int resourceMinedPerInterval;

        public PathNodeAgent PathNodeAgent { get; private set; }

        BehaviorTree<Miner> behaviorTree = new BehaviorTree<Miner>();
        int amountCarried;

        void Awake()
        {
            PathNodeAgent = GetComponent<PathNodeAgent>();
        }

        void Start()
        {
            SetUpBehaviorTree();
        }

        void SetUpBehaviorTree()
        {
            SequenceNode rootSequence = new SequenceNode();
            
            InverterNode inverter = new InverterNode();
            rootSequence.AddChild(inverter);

            ConditionalNode isDead = new DeathCondition<Miner>(this);
            inverter.AddChild(isDead);

            SelectorNode mainSelector = new SelectorNode(); 
            rootSequence.AddChild(mainSelector);

            SequenceNode miningSequence = new SequenceNode();
            mainSelector.AddChild(miningSequence);
            
            SequenceNode depositingSequence = new SequenceNode();
            mainSelector.AddChild(depositingSequence);

            ConditionalNode isBagFull = new BagFullCondition<Miner>(this);
            miningSequence.AddChild(isBagFull);

            LeafNode walkToMine = new WalkToAction<Miner>(this, targetMiningPoint.position, targetMiningPoint.gameObject.name);
            miningSequence.AddChild(walkToMine);

            LeafNode mine = new MineAction<Miner>(this, mineIntervals, resourceMinedPerInterval);
            miningSequence.AddChild(walkToMine);
            
            LeafNode walkToBase = new WalkToAction<Miner>(this, targetDeployPoint.position, targetDeployPoint.gameObject.name);
            depositingSequence.AddChild(walkToBase);
            
            LeafNode deposit = new DepositAction<Miner>(this, depositDuration);
            depositingSequence.AddChild(deposit);
        }

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