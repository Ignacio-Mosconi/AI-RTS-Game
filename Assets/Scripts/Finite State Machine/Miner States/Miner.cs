using UnityEngine;
using UnityEngine.Events;
using GreenNacho.AI.Fsm;
using GreenNacho.AI.Pathfinding;

namespace MinerFsm
{
    [RequireComponent(typeof(PathNodeAgent))]
    public class Miner : MonoBehaviour
    {
        [Header("Miner States")]
        [SerializeField] MinerIdle minerIdle = new MinerIdle();
        [SerializeField] MinerGoingToMine minerGoingToMine = new MinerGoingToMine();
        [SerializeField] MinerMining minerMining = new MinerMining();
        [SerializeField] MinerCarryingGold minerCarryingGold = new MinerCarryingGold();
        [SerializeField] MinerDepositingGold minerDepositingGold = new MinerDepositingGold();

        [Header("Common Attributes")]
        [SerializeField, Range(50, 100)] int maxGoldCarry = 75;

        public PathNodeAgent Agent { get; private set; }

        public UnityEvent OnTimeOut { get; private set; } = new UnityEvent();
        public UnityEvent OnReachDestination { get; private set; } = new UnityEvent();
        public UnityEvent OnFinishManipulatingGold { get; private set; } = new UnityEvent();

        FiniteStateMachine<Miner> fsm;

        void Awake()
        {
            Agent = GetComponent<PathNodeAgent>();

            minerIdle.Initialize(this);
            minerGoingToMine.Initialize(this);
            minerMining.Initialize(this);
            minerCarryingGold.Initialize(this);
            minerDepositingGold.Initialize(this);
        }

        void Start()
        {
            FsmState<Miner>[] states = { minerIdle, minerGoingToMine, minerMining, minerCarryingGold, minerDepositingGold };
            UnityEvent[] events = { OnTimeOut, OnReachDestination, OnFinishManipulatingGold };
            fsm = new FiniteStateMachine<Miner>(states, events, minerIdle);
            
            fsm.SetTransitionRelation(minerIdle, minerGoingToMine, OnTimeOut);
            fsm.SetTransitionRelation(minerGoingToMine, minerMining, OnReachDestination);
            fsm.SetTransitionRelation(minerMining, minerCarryingGold, OnFinishManipulatingGold);
            fsm.SetTransitionRelation(minerCarryingGold, minerDepositingGold, OnReachDestination);
            fsm.SetTransitionRelation(minerDepositingGold, minerIdle, OnTimeOut);
        }

        void Update()
        {
            fsm.UpdateCurrentState();
        }

        public int MaxGoldCarry
        {
            get { return maxGoldCarry; }
        }
    
    }
}