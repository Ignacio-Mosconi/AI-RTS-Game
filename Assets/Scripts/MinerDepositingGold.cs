using UnityEngine;

[System.Serializable]
public class MinerDepositingGold : FsmState<Miner>
{
    [SerializeField, Range(0f, 2f)] float depositTimeOut = 1f;

    float depositTimer;

    public override void EnterState()
    {
        Debug.Log("Entered Depositing Gold");
        depositTimer = 0f;
    }

    public override void UpdateState()
    {
        depositTimer += Time.deltaTime;

        if (depositTimer >= depositTimeOut)
            owner.OnTimeOut.Invoke();
    }
}