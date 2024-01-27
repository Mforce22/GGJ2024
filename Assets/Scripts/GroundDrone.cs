using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDrone : MonoBehaviour
{

    [SerializeField]
    private float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        Move();
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

    bool willFall()
    {
        RaycastHit hit;
        Vector3 obstacleDirection = transform.right + transform.up * -1;
        if (!Physics.Raycast(transform.position, obstacleDirection, out hit, speed * Time.deltaTime * 100))
        {
            Debug.Log("will fall");
            return true;
        }
        return false;
    }

    void turnAround()
    {
        transform.Rotate(0, -180, 0); // TODO: make this a smooth turn
    }
}
