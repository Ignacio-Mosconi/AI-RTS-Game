using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Pathfinding
{
    public class PathfindingManager : MonoBehaviour
    {
        public enum PathfindingAlgorithm
        {
            BreathFirst,
            DepthFirst,
            Dijkstra,
            AStar
        }

        #region Singleton

        static PathfindingManager instance;

        void SetUpSingleton()
        {
            if (Instance == this)
                DontDestroyOnLoad(gameObject);
            else
                Destroy(gameObject);
        }

        public static PathfindingManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<PathfindingManager>();
                if (!instance)
                {
                    GameObject pathFindingManager = new GameObject("Pathfinding Manager");
                    instance = pathFindingManager.AddComponent<PathfindingManager>();
                }

                return instance;
            }
        }

        #endregion

        [SerializeField] PathNodeGenerator pathNodeGenerator = new PathNodeGenerator();
        [SerializeField] PathfindingAlgorithm pathfindingAlgorithm = PathfindingAlgorithm.BreathFirst;

        List<PathNode> pathNodes = new List<PathNode>();
        List<PathNode> openNodes = new List<PathNode>();
        List<PathNode> closedNodes = new List<PathNode>();

        void Awake()
        {
            SetUpSingleton();
        }

        void Start()
        {
            pathNodes = pathNodeGenerator.GeneratePathNodes();
        }

        void Update()
        {
            foreach (PathNode pathNode in pathNodes)
                foreach (PathNode adjacent in pathNode.AdjacentNodes)
                    Debug.DrawLine(pathNode.Position, adjacent.Position, Color.red);
        }

        PathNode GetOpenNode(PathNode destinationNode)
        {
            PathNode openNode = null;

            if (openNodes.Count > 0)
            {
                switch (pathfindingAlgorithm)
                {
                    case PathfindingAlgorithm.BreathFirst:
                        openNode = openNodes[0];
                        break;

                    case PathfindingAlgorithm.DepthFirst:
                        openNode = openNodes[openNodes.Count - 1];
                        break;

                    case PathfindingAlgorithm.Dijkstra:
                        openNode = openNodes[0];

                        foreach (PathNode pathNode in openNodes)
                            if (pathNode.AccumulatedCost < openNode.AccumulatedCost)
                                openNode = pathNode;
                        break;

                    case PathfindingAlgorithm.AStar:
                        openNode = openNodes[0];

                        float closestSqrDistToDest = (destinationNode.Position - openNode.Position).sqrMagnitude;

                        foreach (PathNode pathNode in openNodes)
                        {
                            float sqrDistToDest = (destinationNode.Position - pathNode.Position).sqrMagnitude;

                            if (pathNode.AccumulatedCost < openNode.AccumulatedCost && sqrDistToDest < closestSqrDistToDest)
                            {
                                closestSqrDistToDest = sqrDistToDest;
                                openNode = pathNode;
                            }
                        }
                        break;
                }
            }

            return openNode;
        }

        PathNode GetClosestNode(Vector3 position)
        {
            PathNode closestNode = null;
            float closestSqrDistance = float.MaxValue;

            foreach (PathNode pathNode in pathNodes)
            {
                float sqrDistance = (pathNode.Position - position).sqrMagnitude;

                if (sqrDistance < closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closestNode = pathNode;
                }
            }

            return closestNode;
        }

        void OpenNode(PathNode node)
        {
            node.State = NodeState.Open;
            openNodes.Add(node);
        }

        void CloseNode(PathNode node)
        {
            node.State = NodeState.Closed;
            openNodes.Remove(node);
            closedNodes.Add(node);
        }

        void OpenAdjacentNodes(PathNode parentNode)
        {
            foreach (PathNode pathNode in parentNode.AdjacentNodes)
                if (pathNode.State == NodeState.Unreviewed)
                {
                    float sqrDistance = (parentNode.Position - pathNode.Position).sqrMagnitude;

                    pathNode.Parent = parentNode;
                    pathNode.AccumulatedCost += parentNode.AccumulatedCost + sqrDistance;
                    OpenNode(pathNode);
                }
        }

        void ResetNodes()
        {
            foreach (PathNode pathNode in openNodes)
            {
                pathNode.State = NodeState.Unreviewed;
                pathNode.Parent = null;
                pathNode.AccumulatedCost = pathNode.Cost;
            }
            foreach (PathNode pathNode in closedNodes)
            {
                pathNode.State = NodeState.Unreviewed;
                pathNode.Parent = null;
                pathNode.AccumulatedCost = pathNode.Cost;
            }

            openNodes.Clear();
            closedNodes.Clear();
        }

        void FillPath(PathNode destinationNode, out Stack<PathNode> path)
        {
            PathNode node = destinationNode;
            path = new Stack<PathNode>();

            while (node.Parent != null)
            {
                path.Push(node);
                node = node.Parent;
            }
        }

        public Stack<PathNode> CreatePath(Vector3 origin, Vector3 destination)
        {
            Stack<PathNode> path = null;
            PathNode originNode = GetClosestNode(origin);
            PathNode destinationNode = GetClosestNode(destination);
            bool foundDestination = false;

            OpenNode(originNode);

            while (openNodes.Count > 0 && !foundDestination)
            {
                PathNode pathNode = GetOpenNode(destinationNode);

                if (pathNode == destinationNode)
                {
                    foundDestination = true;
                    FillPath(pathNode, out path);
                }
                CloseNode(pathNode);
                if (!foundDestination)
                    OpenAdjacentNodes(pathNode);
            }

            ResetNodes();

            return path;
        }
    }
}