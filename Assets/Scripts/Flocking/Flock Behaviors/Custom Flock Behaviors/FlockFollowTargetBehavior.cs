using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public class FlockFollowTargetBehavior : FlockCompositeBehavior
    {
        [Header("Additional Behaviors")]
        [SerializeField] FollowTargetBehavior followTargetBehavior = default;

        public override void OnInitialize()
        {
            flockBehaviors.Add(followTargetBehavior);
        }
    }
}