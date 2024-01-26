using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrone : MonoBehaviour
{

    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float viewCheckTime = 2f;
    [SerializeField]
    private float chaseTime = 5f;
    [SerializeField]
    private float viewPlayerRange = 15f;
    [SerializeField]
    private float viewObstacleRange = 15f;
    [SerializeField]

    private GameObject player;

    private bool isChasing = false;

    void Start()
    {
        // use InvokeRepeating to launch seePlayer() 2 seconds
        // after the game starts, and every 2 seconds after that
        InvokeRepeating("checkStartChase", viewCheckTime, viewCheckTime);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        // move the drone forward
        if (!canSeeObstacle())
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            // if the drone sees an obstacle, turn around
            turnAround();
        }
    }

    void checkStartChase()
    {
        // raycast from the drone to the player
        RaycastHit hit;
        Vector3 playerDirection = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, playerDirection, out hit, viewPlayerRange))
        {
            // if the raycast hits the player, return true
            if (hit.collider.GetComponent<Platform>() != null)
            {
                if (!isChasing)
                {
                    // starting chase
                    Debug.Log("I CAN SEE YOU");
                    isChasing = true;
                    speed *= 2;
                    Invoke("stopChase", chaseTime);
                }
            }
        }
    }

    bool canSeeObstacle()
    {
        RaycastHit hit;
        Vector3 obstacleDirection = transform.right;
        if (Physics.Raycast(transform.position, obstacleDirection, out hit, viewObstacleRange))
        {
            // if the raycast hits an obstacle, return true
            if (hit.collider.gameObject.tag != "Obstacle")
            {
                return true;
            }
        }
        // one in 20 chance of printing hit
        Debug.DrawRay(transform.position, obstacleDirection * viewObstacleRange, Color.red);
        return false;
    }

    void stopChase()
    {
        isChasing = false;
        speed /= 2;
    }

    void turnAround()
    {
        transform.Rotate(0, -180, 0); // TODO: make this a smooth turn
    }
}
