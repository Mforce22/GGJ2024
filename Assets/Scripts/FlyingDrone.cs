using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrone : MonoBehaviour
{

    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float viewCheckTime = .5f;
    [SerializeField]
    private float chaseTime = 5f;
    [SerializeField]
    private float viewPlayerRange = 5f;
    [SerializeField]
    private float viewObstacleRange = 1f;
    [SerializeField]
    private float rotationSpeed = 250f;

    private Quaternion desiredRotation;

    private GameObject player;

    private bool isChasing = false;

    void Start()
    {
        // use InvokeRepeating to launch seePlayer() 2 seconds
        // after the game starts, and every 2 seconds after that
        player =
                GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("checkStartChase", viewCheckTime, viewCheckTime);
        desiredRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(transform.rotation.eulerAngles.y, desiredRotation.eulerAngles.y))
        {
            Move();
        }
        else
        {
            Rotate();
        }
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

    void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }

    void checkStartChase()
    {
        // raycast from the drone to the player
        RaycastHit hit;
        // Vector3 playerDirection = transform.right;
        Vector3 playerDirection = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, playerDirection, out hit, viewPlayerRange))
        {
            Debug.Log(hit.collider.gameObject.tag);
            // if the raycast hits the player, return true
            if (hit.collider.gameObject.tag == "Player")
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
            if (hit.collider.gameObject.tag == "Platform")
            {
                return true;
            }
        }
        return false;
    }

    void stopChase()
    {
        isChasing = false;
        speed /= 2;
    }

    void turnAround()
    {
        desiredRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
    }
}
