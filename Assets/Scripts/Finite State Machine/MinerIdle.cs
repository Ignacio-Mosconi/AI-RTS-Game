using UnityEngine;
using GreenNacho.AI.Fsm;

[System.Serializable]
public class MinerIdle : FsmState<Miner>
{
    [SerializeField, Range(0f, 5f)] float idleTimeOut = 2.5f;

    float idleTimer;

    public override void EnterState()
    {
        idleTimer = 0f;
    }

    public override void UpdateState()
    {
        idleTimer += Time.deltaTime;
        
        if (idleTimer >= idleTimeOut)
            owner.OnTimeOut.Invoke();
    }
}