using UnityEngine;

[System.Serializable]
public class MinerMining : FsmState<Miner>
{
    [SerializeField, Range(1f, 3f)] float miningIntevals = 2f;
    [SerializeField, Range(10, 50)] int goldMinedPerInterval = 20;

    float mineTimer;
    int minedGold;

    public override void EnterState()
    {
        mineTimer = 0f;
        minedGold = 0;
    }

    public override void UpdateState()
    {
        mineTimer += Time.deltaTime;

        if (mineTimer >= miningIntevals)
        {
            mineTimer = 0f;
            minedGold += goldMinedPerInterval;

            if (minedGold >= owner.MaxGoldCarry)
            {
                minedGold = owner.MaxGoldCarry;
                owner.OnFinishManipulatingGold.Invoke();
            }
        }
    }
}