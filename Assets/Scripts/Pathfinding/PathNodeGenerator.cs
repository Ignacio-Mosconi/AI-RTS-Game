using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Pathfinding
{
    [System.Serializable]
    public class PathNodeGenerator
    {
        [SerializeField, Range(-1000f, 0f)] float minPosX = -500f;
        [SerializeField, Range(0f, 1000f)] float maxPosX = 500f;
        [SerializeField, Range(-1000f, 0f)] float minPosZ = -500f;
        [SerializeField, Range(0f, 1000f)] float maxPosZ = 500f;
        [SerializeField] float pathNodeDistance = 10f;
        [SerializeField] string terrainTag = "Terrain";
        [SerializeField] string dynamicObstacleTag = "DynamicObstacle";

        float hypothenuseDistance;
        
        const float RayDistance = 1000f;
        const float NegligibleDistance = 0.1f;

        bool AreNodesAdjacent(PathNode firstNode, PathNode secondNode)
        {
            Vector3 diff = secondNode.Position - firstNode.Position;

            float sqrDistance = diff.sqrMagnitude;
            bool areConnected = !Physics.Raycast(firstNode.Position, diff, hypothenuseDistance);

            return (areConnected && 
                    sqrDistance - NegligibleDistance <= hypothenuseDistance * hypothenuseDistance 
                    && firstNode != secondNode);
        }

        public List<PathNode> GeneratePathNodes()
        {   
            List<PathNode> pathNodes = new List<PathNode>();

            for (float z = minPosZ; z <= maxPosZ; z += pathNodeDistance)
                for (float x = minPosX; x <= maxPosX; x += pathNodeDistance)
                {
                    RaycastHit hitInfo; 
                    Vector3 rayOrigin = new Vector3(x, RayDistance, z);
                    
                    if (Physics.Raycast(rayOrigin, -Vector3.up, out hitInfo, RayDistance))
                    {
                        PathNode pathNode = null;
                        
                        if (hitInfo.collider.tag == terrainTag)
                            pathNode = new PathNode(hitInfo.point);

                        if (hitInfo.collider.tag == dynamicObstacleTag)
                            pathNode = new PathNode(hitInfo.point, canBeBlocked: true);

                        if (pathNode != null)
                            pathNodes.Add(pathNode);
                    }
                }

            float legSquared = pathNodeDistance * pathNodeDistance;
            hypothenuseDistance = Mathf.Sqrt(legSquared + legSquared);

            foreach (PathNode pathNode in pathNodes)
                pathNode.AdjacentNodes = pathNodes.FindAll(n => AreNodesAdjacent(pathNode, n));

            return pathNodes;
        }

        public float HypothenuseSqrDistance
        {
            get { return hypothenuseDistance * hypothenuseDistance; }
        }
    }
}