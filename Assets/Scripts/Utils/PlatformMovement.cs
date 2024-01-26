using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField]
    private float speedX;

    [SerializeField] 
    private float speedY;

    [SerializeField]
    private float distanceX;

    [SerializeField]
    private float distanceY;

    void Update()
    {
        if(speedX > 0 && distanceX > 0)
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time * speedX, distanceX), transform.position.y, transform.position.z);
        }
        else if(speedY > 0 && distanceY >0)
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speedY, distanceY), transform.position.z);
        }
    }
}
