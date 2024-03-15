using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ClydeChase : GhostChase
{
    protected float distanceToGhost(Vector3 position)
    {
        Vector3 ghostPosition = getClosestGhostPosition();

        return Math.Abs(ghostPosition.x - position.x) + Math.Abs(ghostPosition.y - position.y);
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

            // Obtem a Direcao do Fastama mais perto e faz ser o inverso 
            // Talvez seja preciso uma verificaçao que ele nao faz o reverso
            Vector2 bestDirection = Vector2.zero;
            float shortestDistance = float.MinValue;

            // Loop through each available direction
            foreach (Vector2 dir in dirs)
            {
                // Do not allow going back
                if (dir != -currentDirection())
                {
                    // Calculate the position if the ghost moves in this direction
                    Vector3 newPosition = transform.position + new Vector3(dir.x, dir.y, 0f);

                    // Calculate the distance from this position to Ghost
                    float distance = distanceToGhost(newPosition);

                    // Check if this direction takes the ghost closer to the player
                    if (distance > shortestDistance)
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

