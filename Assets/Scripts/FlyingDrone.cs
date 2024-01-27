using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDrone : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float viewCheckTime = .5f;
    [SerializeField]
    private float chaseTime = 5f;
    [SerializeField]
    private float viewPlayerRange = 7f;
    [SerializeField]
    private float viewObstacleRange = 2f;
    [SerializeField]
    private float rotationSpeed = 500f;

    private Quaternion desiredRotation;
    private Quaternion wingsDesiredRotation = Quaternion.Euler(0, 0, 60);
    [SerializeField]
    private float wingsRotationSpeed = 500f;
    [SerializeField]
    private Transform wingsTransform;

    private Quaternion bodyDesiredRotation = Quaternion.Euler(30, 0, 0);
    [SerializeField]
    private float bodyRotationSpeed = 100f;
    [SerializeField]
    private Transform bodyTransform;


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
        Animate();
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

    void Animate()
    {
        if (Mathf.Abs(wingsTransform.localRotation.eulerAngles.z - wingsDesiredRotation.eulerAngles.z) < 0.01f)
        {
            if (wingsDesiredRotation.eulerAngles.z == 300)
            {
                wingsDesiredRotation = Quaternion.Euler(0, 0, 60);
            }
            else
            {
                wingsDesiredRotation = Quaternion.Euler(0, 0, 300);
            }
        }
        wingsTransform.localRotation = Quaternion.RotateTowards(wingsTransform.localRotation, wingsDesiredRotation, wingsRotationSpeed * Time.deltaTime);

        if (Mathf.Abs(bodyTransform.localRotation.eulerAngles.x - bodyDesiredRotation.eulerAngles.x) < 0.01f)
        {
            if (bodyDesiredRotation == Quaternion.Euler(30, 0, 0))
            {
                bodyDesiredRotation = Quaternion.Euler(330, 0, 0);
            }
            else
            {
                bodyDesiredRotation = Quaternion.Euler(30, 0, 0);
            }
        }
        bodyTransform.localRotation = Quaternion.RotateTowards(bodyTransform.localRotation, bodyDesiredRotation, bodyRotationSpeed * Time.deltaTime);
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
