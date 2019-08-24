﻿using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Unreviewed,
    Open,
    Closed
}

public class PathNode
{
    public List<PathNode> AdjacentNodes { get; set; }
    public PathNode Parent { get; set; }
    public NodeState State { get; set; }
    public Vector3 Position {get; private set; }
    public bool CanBeBlocked { get; private set; }

    public PathNode(Vector3 position, bool canBeBlocked = false)
    {
        AdjacentNodes = new List<PathNode>();
        Parent = null;
        State = NodeState.Unreviewed;
        Position = position;
        CanBeBlocked = canBeBlocked;
    }
}