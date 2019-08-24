using UnityEngine;

[System.Serializable]
public class MinerCarryingGold : FsmState<Miner>
{
    [SerializeField] Transform targetDeployPoint = default;

    Transform minerTransform;
    Vector3 targetPosition;

    public override void Initialize(Miner owner)
    {
        base.Initialize(owner);
        minerTransform = owner.transform;
    }

    public override void EnterState()
    {
        targetPosition = targetDeployPoint.position;
    }

    public override void ExitState()
    {
        targetPosition = Vector3.zero;
    }

    public override void UpdateState()
    {
        Vector3 currentPosition = minerTransform.position;
        float minerSpeed = owner.MovementSpeed * Time.deltaTime * 0.5f;

        minerTransform.position = Vector3.MoveTowards(currentPosition, targetPosition, minerSpeed);

        if (minerTransform.position == targetPosition)
            owner.OnReachDestination.Invoke();
    }
}