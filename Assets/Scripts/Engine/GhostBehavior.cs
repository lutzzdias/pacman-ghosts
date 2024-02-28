using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;
using Unity.VisualScripting;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float duration;
    private GameObject gameManagerObject;
    private GameObject pacmanObject;
    private GameObject blinkyObject;
    private GameObject pinkyObject;
    private GameObject inkyObject;
    private GameObject clydeObject;


    private void Awake()
    {
        ghost = GetComponent<Ghost>();

        gameManagerObject = GameObject.Find("GameManager");
        pacmanObject = GameObject.Find("Pacman");
        blinkyObject = GameObject.Find("Ghost_Blinky");
        pinkyObject = GameObject.Find("Ghost_Pinky");
        inkyObject = GameObject.Find("Ghost_Inky");
        clydeObject = GameObject.Find("Ghost_Clyde");
    }

    public virtual void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;

        CancelInvoke();
    }


    /// Sensors ///

    protected bool isFrightened()
    {
        return ghost.frightened.enabled;
    }

    protected bool atHome()
    {
        return ghost.home.enabled;
    }

    protected bool isChasing()
    {
        return ghost.chase.enabled;
    }

    protected Vector3 getPacmanPosition()
    {
        return new Vector3(pacmanObject.transform.position.x, pacmanObject.transform.position.y, pacmanObject.transform.position.z);
    }

    protected Vector2 getPacmanDirection()
    {
        Vector2 pacmanDirection = pacmanObject.GetComponent<Movement>().direction;
        return new Vector2(pacmanDirection.x, pacmanDirection.y);
    }
    protected float getPacmanDistance()
    {
        return Math.Abs(pacmanObject.transform.position.x - transform.position.x) + Math.Abs(pacmanObject.transform.position.y - transform.position.y);
    }

    protected int getRemainingLives()
    {
        return gameManagerObject.GetComponent<GameManager>().Lives;
    }

    protected int getCurrentLevel(){
        return gameManagerObject.GetComponent<GameManager>().level;
    }

    protected Vector3[] getRemainingPellets()
    {
        GameManager gm = gameManagerObject.GetComponent<GameManager>();
        Transform[] aux = gm.GetRemainingPellets();
        Vector3[] vec = new Vector3[aux.Length];
        for (int i = 0; i < aux.Length; i++)
        {
            vec[i] = aux[i].position;
        }
        return vec;
    }

    protected Vector3 getClosestPelletPosition()
    {
        Vector3[] pellets = getRemainingPellets();
        Vector3 closest = pellets[0];
        float closestDistance = 0;
        foreach (Vector3 p in pellets)
        {
            float distance = Math.Abs(transform.position.x - p.x) + Math.Abs(transform.position.y - p.y);
            if (closest == null || distance < closestDistance)
            {
                closest = p;
                closestDistance = distance;
            }
        }
        return closest;
    }

    protected float getClosestPelletDistance()
    {
        Vector3 closest = getClosestPelletPosition();
        return Math.Abs(transform.position.x - closest.x) + Math.Abs(transform.position.y - closest.y);
    }

    protected int countRemainingPellets()
    {
        GameManager gm = gameManagerObject.GetComponent<GameManager>();
        return gm.CountRemainingPellets();
    }

    private GameObject closestGhost()
    {
        GameObject[] ghosts = { blinkyObject, inkyObject, pinkyObject, clydeObject };
        GameObject closest = null;
        float smallestDistance = 0;
        foreach (GameObject g in ghosts)
        {            
            if (g != this.gameObject)
            {
                
                float distance = Math.Abs(g.transform.position.x-transform.position.x) + Math.Abs(g.transform.position.y - transform.position.y);
                if (closest == null || distance < smallestDistance)
                {
                    closest = g;
                    smallestDistance = distance;
                }
            }
        }
        return closest;
    }

    protected Vector3 getClosestGhostPosition()
    {
        return closestGhost().transform.position;
    }

    protected Vector2 getClosestGhostDirection()
    {
        return closestGhost().GetComponent<Movement>().direction;
    }

    protected float getClosestGhostDistance()
    {
        Vector3 closest = getClosestGhostPosition();
        return Math.Abs(transform.position.x - closest.x) + Math.Abs(transform.position.y - closest.y);
    }

    protected List<Vector2> getAvailableDirections(Node node)
    {
        return node.availableDirections;
    }

    protected bool canGoUp(Node node)
    {
        Vector2 up = new Vector2(0, 1);
        foreach (Vector2 availableDirection in node.availableDirections)
        {
            if (availableDirection == up)
                return true;
        }
        return false;
    }

    protected bool canGoDown(Node node)
    {
        Vector2 up = new Vector2(0, -1);
        foreach (Vector2 availableDirection in node.availableDirections)
        {
            if (availableDirection == up)
                return true;
        }
        return false;
    }

    protected bool canGoLeft(Node node)
    {
        Vector2 up = new Vector2(-1, 0);
        foreach (Vector2 availableDirection in node.availableDirections)
        {
            if (availableDirection == up)
                return true;
        }
        return false;
    }

    protected bool canGoRight(Node node)
    {
        Vector2 up = new Vector2(1, 0);
        foreach (Vector2 availableDirection in node.availableDirections)
        {
            if (availableDirection == up)
                return true;
        }
        return false;
    }

    protected Vector2 currentDirection()
    {
        return ghost.movement.direction;
    }

    protected Vector3 currentPosition()
    {
        return transform.position;
    }


    /// Actions ///
    protected void setDirection(Vector2 direction)
    {
        ghost.movement.SetDirection(direction);   
    }




}