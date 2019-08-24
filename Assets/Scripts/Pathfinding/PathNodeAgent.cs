using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PathNodeAgent : MonoBehaviour
{
    [SerializeField, Range(2f, 10f)] float movementSpeed = 5f;

    public bool HasReachedDestination { get; private set; } = false;

    Stack<PathNode> path;
    PathNode currentTargetNode;
    Vector3 destinationPosition;
    Vector3 currentTargetPosition;
    Vector3 colliderHeightOffset;
    Collider agentCollider;

    void Awake()
    {
        agentCollider = GetComponent<Collider>();
    }

    void Start()
    {
        CapsuleCollider capsuleCollider = agentCollider as CapsuleCollider;
        BoxCollider boxCollider = agentCollider as BoxCollider;
        
        if (capsuleCollider)
            colliderHeightOffset = new Vector3(0f, capsuleCollider.height * 0.5f, 0f);
        if (boxCollider)
            colliderHeightOffset = new Vector3(0f, boxCollider.size.y * 0.5f, 0f);
    }

    public void SimpleMove(Vector3 destinationPosition)
    {
        Vector3 currentPosition = transform.position;
        float maxDistanceDelta = movementSpeed * Time.deltaTime;

        if (this.destinationPosition != destinationPosition)
        {
            this.destinationPosition = destinationPosition;
            path = PathfindingManager.Instance.CreatePath(currentPosition, destinationPosition);
            currentTargetNode = path.Pop();
            HasReachedDestination = false;
        }
        
        currentTargetPosition = currentTargetNode.Position + colliderHeightOffset;
        transform.position = Vector3.MoveTowards(currentPosition, currentTargetPosition, maxDistanceDelta);
        
        if (transform.position == currentTargetPosition)
        {
            if (path.Count > 0)
                currentTargetNode = path.Pop();
            else
                HasReachedDestination = true;
        }
    }

    public float MovementSpeed
    {
        get { return movementSpeed; }
    }
}