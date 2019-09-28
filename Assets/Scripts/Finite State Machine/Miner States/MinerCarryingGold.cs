using UnityEngine;
using GreenNacho.AI.Fsm;
using GreenNacho.AI.Pathfinding;

namespace MinerFsm
{
    [System.Serializable]
    public class MinerCarryingGold : FsmState<Miner>
    {
        [SerializeField] Transform targetDeployPoint = default;

        PathNodeAgent agent;
        Vector3 targetPosition;

        public override void Initialize(Miner owner)
        {
            base.Initialize(owner);
            agent = owner.Agent;
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
            agent.SimpleMove(targetPosition);

            if (agent.HasReachedDestination)
                owner.OnReachDestination.Invoke();
        }
    }  
}