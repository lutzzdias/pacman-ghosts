using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyChase : GhostChase
{
    protected float distanceToPacman(Vector3 position)
    {
        Vector3 pacmanPosition = getPacmanPosition();

        return Math.Abs(pacmanPosition.x - position.x) + Math.Abs(pacmanPosition.y - position.y);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Instantiate decision node object
        Node node = other.GetComponent<Node>();

        // Check that the node exists and the ghost is in the correct behavior (chase)
        if (node != null && isChasing() && !isFrightened())
        {
            // Get available directions
            List<Vector2> dirs = getAvailableDirections(node);

            // Keep track of the best direction and distance
            Vector2 bestDirection = Vector2.zero;
            float shortestDistance = float.MaxValue;
            
            // Loop through each available direction
            foreach (Vector2 dir in dirs)
            {
                // Do not allow going back
                if (dir != -currentDirection())
                {
                    // Calculate the position if the ghost moves in this direction
                    Vector3 newPosition = transform.position + new Vector3(dir.x, dir.y, 0f);

                    // Calculate the distance from this position to pacman
                    float distance = distanceToPacman(newPosition);

                    // Check if this direction takes the ghost closer to the player
                    if (distance < shortestDistance)
                    {
                        bestDirection = dir;
                        shortestDistance = distance;
                    }
                }

            }

            setDirection(bestDirection);
        }
    }
}