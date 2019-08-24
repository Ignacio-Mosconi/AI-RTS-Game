using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    #region Singleton

    static PathfindingManager instance;

    void SetUpSingleton()
    {
        if (Instance == this)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    public PathfindingManager Instance
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
    
    List<PathNode> pathNodes;

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
}