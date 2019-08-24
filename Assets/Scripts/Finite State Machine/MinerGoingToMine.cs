using UnityEngine;

[System.Serializable]
public class MinerGoingToMine : FsmState<Miner>
{
    [SerializeField] Transform targetMiningPoint = default;

    Transform minerTransform;
    Vector3 targetPosition;

    public override void Initialize(Miner owner)
    {
        base.Initialize(owner);
        minerTransform = owner.transform;
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
        Vector3 currentPosition = minerTransform.position;
        float minerSpeed = owner.MovementSpeed * Time.deltaTime;
        
        minerTransform.position = Vector3.MoveTowards(currentPosition, targetPosition, minerSpeed);

        if (minerTransform.position == targetPosition)
            owner.OnReachDestination.Invoke();
    }
}