using UnityEngine;

[System.Serializable]
public class MinerGoingToMine : FsmState<Miner>
{
    [SerializeField] Transform targetMiningPoint = default;

    PathNodeAgent agent;
    Vector3 targetPosition;

    public override void Initialize(Miner owner)
    {
        base.Initialize(owner);
        agent = owner.Agent;
    }

    public override void EnterState()
    {
        targetPosition = targetMiningPoint.position;
    }

    public override void ExitState()
    {
        targetPosition = Vector3.zero;
    }

    public override void UpdateState()
    {
        agent.SimpleMove(targetPosition);

        if (agent.HasReachedDestination)
            owner.OnReachDestination.Invoke();
    }
}