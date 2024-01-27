using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDrone : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 250f;
    private Quaternion desiredRotation;

    void Start()
    {
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
        if (!willFall())
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

    bool willFall()
    {
        RaycastHit hit;
        Vector3 obstacleDirection = transform.right + transform.up * -1;
        if (!Physics.Raycast(transform.position, obstacleDirection, out hit, 5f))
        {
            Debug.Log("will fall");
            return true;
        }
        return false;
    }

    void turnAround()
    {
        desiredRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
    }
}
