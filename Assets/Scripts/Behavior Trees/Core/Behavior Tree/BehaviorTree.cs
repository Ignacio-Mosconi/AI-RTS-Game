using UnityEngine;

namespace GreenNacho.AI.BehaviorTrees
{
    [System.Serializable]
    public class BehaviorTree<T> where T : MonoBehaviour
    {
        BranchNode rootNode;

        void AddNode(BehaviorNode behaviorNode, BranchNode parentNode)
        {
            if (parentNode != null)
                parentNode.AddChild(behaviorNode);
            else
            {
                if (rootNode != null)
                    rootNode.AddChild(behaviorNode);
                else
                {
                    rootNode = behaviorNode as BranchNode;
                    if (rootNode == null)
                        Debug.LogError("Attempted to set a leaf node as root.");
                }
            }
        }

        public SequenceNode AddSequence(BranchNode parentNode = null)
        {
            SequenceNode sequenceNode = new SequenceNode();

            AddNode(sequenceNode, parentNode);

            return sequenceNode;
        }

        public SelectorNode AddSelector(BranchNode parentNode = null)
        {
            SelectorNode selectorNode = new SelectorNode();

            AddNode(selectorNode, parentNode);

            return selectorNode;
        }

        public LogicNode AddLogicNode(BranchNode parentNode)
        {
            LogicNode logicNode = new LogicNode();

            AddNode(logicNode, parentNode);

            return logicNode;
        }

        
    }
}