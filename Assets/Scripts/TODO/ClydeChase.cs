using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ClydeChase : GhostChase
{
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
            Vector2 dir = getClosestGhostDirection();
            Vector2 BestDirection = -dir;

            // Check if BestDirection is in dirs
            if (dirs.Contains(BestDirection))
            {
                setDirection(BestDirection);
            }
            else
            {
                // Generate a random direction 
                // Fiz isto por enquanto para nao ficar preso na parede sem saber o que fazer
                int count = dirs.Count;
                int i = Random.Range(0, count);
                if (count > 1 && dirs[i] == -currentDirection())
                {
                    i = (i + 1) % count;
                }
                setDirection(dirs[i]);
            }
        }
    }
}

