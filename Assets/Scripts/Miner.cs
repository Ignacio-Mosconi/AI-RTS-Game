using UnityEngine;
using UnityEngine.Events;

public class Miner : MonoBehaviour
{
    [Header("Miner States")]
    [SerializeField] MinerIdle minerIdle = new MinerIdle();
    [SerializeField] MinerGoingToMine minerGoingToMine = new MinerGoingToMine();
    [SerializeField] MinerMining minerMining = new MinerMining();
    [SerializeField] MinerCarryingGold minerCarryingGold = new MinerCarryingGold();
    [SerializeField] MinerDepositingGold minerDepositingGold = new MinerDepositingGold();

    [Header("Common Attributes")]
    [SerializeField, Range(2f, 10f)] float movementSpeed = 5f;
    [SerializeField, Range(50, 100)] int maxGoldCarry = 75;

    public UnityEvent OnTimeOut { get; private set; } = new UnityEvent();
    public UnityEvent OnReachDestination { get; private set; } = new UnityEvent();
    public UnityEvent OnFinishManipulatingGold { get; private set; } = new UnityEvent();

    FiniteStateMachine<Miner> fsm;

    void Awake()
    {
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

    public float MovementSpeed
    {
        get { return movementSpeed; }
    }

    public int MaxGoldCarry
    {
        get { return maxGoldCarry; }
    }
}