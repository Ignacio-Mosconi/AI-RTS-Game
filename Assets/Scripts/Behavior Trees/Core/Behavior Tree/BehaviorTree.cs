using UnityEngine;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class BehaviorTree<T> where T : MonoBehaviour
    {
        BranchNode rootNode;

        public void AddRoot(BranchNode rootNode)
        {
            if (rootNode != null)
                this.rootNode = rootNode; 
        }

        public void Update()
        {
            rootNode.UpdateState();
        }
    }
}