using UnityEngine;

public class GhostChase : GhostBehavior
{
    public override void Disable()
    {
        enabled = false;
        CancelInvoke();
    }


    public override void Enable()
    {
        enabled = true;
        CancelInvoke();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Sample code for the chase behavior
        //We only have to select the next direction to move to
    
        //instantiating the intersection node object
        Node node = other.GetComponent<Node>(); 

        //First check if this behaviour is enabled and the ghost is not frightened
        if (node != null && isChasing() && !isFrightened())
        {
            //Go through the available directions and choose the first that
            //is not opposite to the current direction
            foreach (Vector2 availableDirection in getAvailableDirections(node))
            {
                if (availableDirection != -currentDirection()) 
                {
                    setDirection(availableDirection);
                    break;
                }
            }
        }
    }
}
