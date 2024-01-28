using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDrone : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 250f;
    [SerializeField]
    private float tailRotationSpeed = 100f;
    [SerializeField]
    private Transform tailTransform;
    private Quaternion tailDesiredRotation = Quaternion.Euler(0, 0, 0);

    private Quaternion desiredRotation;
    [SerializeField]
    private float viewObstacleWidth = 1f;
    [SerializeField]
    private float viewObstacleRange = 1f;

    void Start()
    {
        desiredRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        if (Mathf.Abs(transform.rotation.eulerAngles.y - desiredRotation.eulerAngles.y) < 0.01f)
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
        if (!willFall() && !canSeeObstacle())
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
        // animate the drone
        if (Mathf.Approximately(tailTransform.localRotation.eulerAngles.z, tailDesiredRotation.eulerAngles.z))
        {
            if (tailDesiredRotation.eulerAngles.z == 0)
            {
                tailDesiredRotation = Quaternion.Euler(0, 0, 70);
            }
            else
            {
                tailDesiredRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        tailTransform.localRotation = Quaternion.RotateTowards(tailTransform.localRotation, tailDesiredRotation, tailRotationSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }

    bool willFall()
    {
        RaycastHit hit;
        Vector3 obstacleDirection = transform.right + transform.up * -1;
        if (!Physics.Raycast(transform.position, obstacleDirection, out hit, 1f))
        {
            Debug.Log("will fall");
            return true;
        }
        return false;
    }

    bool canSeeObstacle()
    {
        RaycastHit hit;
        Vector3 obstacleDirection = transform.right;
        // spherecast to check for obstacles
        if (Physics.SphereCast(transform.position, viewObstacleWidth, obstacleDirection, out hit, viewObstacleRange))
        {
            // if the raycast hits an obstacle, return true
            if (hit.collider.gameObject.tag == "Platform")
            {
                return true;
            }
        }
        return false;
    }

    void turnAround()
    {
        desiredRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
    }
}
