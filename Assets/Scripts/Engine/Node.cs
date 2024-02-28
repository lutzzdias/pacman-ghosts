using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public readonly List<Vector2> availableDirections = new();

    private void Start()
    {
        availableDirections.Clear();

        // We determine if the direction is available by box casting to see if
        // we hit a wall. The direction is added to list if available.
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D[] hits = new RaycastHit2D[1];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;
        filter.useLayerMask = false;
        filter.useDepth = true;
        filter.maxDepth = -0.5f;
        filter.minDepth = -1.5f;
        Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, filter, hits, 1f);


        // If no collider is hit then there is no obstacle in that direction
        if (hits[0].collider == null) {
            availableDirections.Add(direction);
        }
    }

}
