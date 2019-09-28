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
        [SerializeField, Range(0f, 100f)] float maxLife = 100f;
        [SerializeField, Range(1f, 5f)] float mineIntervals = 2.5f;
        [SerializeField, Range(1f, 5f)] float depositDuration = 2.5f;
        [SerializeField, Range(0, 100)] int maxCarryAmount = 50;
        [SerializeField, Range(1, 10)] int resourceMinedPerInterval = 5;

        public PathNodeAgent PathNodeAgent { get; private set; }

        BehaviorTree<Miner> behaviorTree = new BehaviorTree<Miner>();
        float life;
        int amountCarried;

        void Awake()
        {
            PathNodeAgent = GetComponent<PathNodeAgent>();
        }

        void Start()
        {
            life = maxLife;
            amountCarried = 0;
            SetUpBehaviorTree();
        }

        void Update()
        {
            behaviorTree.Update();
        }

        void SetUpBehaviorTree()
        {
            SequenceNode rootSequence = new SequenceNode("Root");
            behaviorTree.AddRoot(rootSequence);
            
            InverterNode isDeadInverter = new InverterNode();
            rootSequence.AddChild(isDeadInverter);

            ConditionalNode isDead = new DeathCondition<Miner>(this);
            isDeadInverter.AddChild(isDead);

            SequenceNode mainSequence = new SequenceNode("Main"); 
            rootSequence.AddChild(mainSequence);

            SequenceNode miningSequence = new SequenceNode("Mining");
            mainSequence.AddChild(miningSequence);
            
            SequenceNode depositingSequence = new SequenceNode("Depositing");
            mainSequence.AddChild(depositingSequence);

            InverterNode isBagFullInverter = new InverterNode();
            miningSequence.AddChild(isDeadInverter);

            ConditionalNode isBagFull = new BagFullCondition<Miner>(this);
            isBagFullInverter.AddChild(isBagFull);

            LeafNode walkToMine = new WalkToAction<Miner>(this, targetMiningPoint.position, targetMiningPoint.gameObject.name);
            miningSequence.AddChild(walkToMine);

            LeafNode mine = new MineAction<Miner>(this, mineIntervals, resourceMinedPerInterval);
            miningSequence.AddChild(mine);
            
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
            set { amountCarried = value; }
        }

        public int MaxCarryAmount
        {
            get { return maxCarryAmount; }
        }
    }
}