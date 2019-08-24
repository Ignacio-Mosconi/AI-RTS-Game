using System.Collections.Generic;
using UnityEngine;

public enum NavigationMeshLimit
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

public class PathNodeGenerator : MonoBehaviour
{
    [SerializeField, Range(0f, 500f)] float minPosX = 0f;
    [SerializeField, Range(500f, 1000f)] float maxPosX = 1000f;
    [SerializeField, Range(0f, 500f)] float minPosZ = 0f;
    [SerializeField, Range(500f, 1000f)] float maxPosZ = 1000f;
    [SerializeField] float pathNodeDistance = 10f;
    [SerializeField] string terrainTag = "Terrain";
    [SerializeField] string dynamicObstacleTag = "DynamicObstacle";

    List<PathNode> pathNodes;
    
    const float RayDistance = 10000f;

    void Start()
    {

    }

    List<PathNode> GeneratePathNodes()
    {   
        List<PathNode> generatedPathNodes = new List<PathNode>();

        for (float z = minPosZ; z < maxPosZ; z += pathNodeDistance)
            for (float x = minPosX; x < maxPosX; z += pathNodeDistance)
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

        foreach (PathNode pathNode in pathNodes)
        {
            RaycastHit hitInfo;
            List<PathNode> adjacentNodes = pathNodes.FindAll(n => AreNodesAdjacent(pathNode, n));

            foreach (PathNode adjacentNode in adjacentNodes)
            {
                Vector3 rayDirection = (adjacentNode.Position - pathNode.Position).normalized;   
                if (Physics.Raycast(pathNode.Position, rayDirection, out hitInfo, pathNodeDistance))
                    adjacentNodes.Remove(adjacentNode);
            }

            pathNode.AdjacentNodes = adjacentNodes;
        }
    }

    bool AreNodesAdjacent(PathNode firstNode, PathNode secondNode)
    {
        float distance = Vector3.Distance(firstNode.Position, secondNode.Position);

        return distance <= pathNodeDistance;
    }
}