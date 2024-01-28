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

    private Transform _transform;
    float movedX = 0;
    float movedY = 0;

    private void Awake()
    {
        _transform = transform;
    }

    void Update()
    {

        if (speedX != 0 && distanceX != 0)
        {
            transform.Translate(new Vector3(speedX * Time.deltaTime, 0, 0));
            movedX += speedX * Time.deltaTime;
            if (movedX >= distanceX)
            {
                if (Mathf.Sign(movedX) == 1)
                {
                    speedX *= -1;
                }
            }
            else if (movedX <= 0)
            {
                if (Mathf.Sign(movedX) == -1)
                {
                    speedX *= -1;
                }
            }

        }
        else if (speedY != 0 && distanceY != 0)
        {
            transform.Translate(new Vector3(0, speedY * Time.deltaTime, 0));
            movedY += speedY * Time.deltaTime;
            //Debug.Log(speedY);
            if (movedY >= distanceY)
            {
                if (Mathf.Sign(speedY) == 1f)
                {
                    //Debug.Log("Cambio");
                    speedY *= -1;
                }
            }
            else if (movedY <= 0)
            {
                if (Mathf.Sign(speedY) == -1f)
                {
                    speedY *= -1;
                }
            }
            //transform.position = new Vector3(transform.position.x, (_transform.position.y + Mathf.PingPong(Time.deltaTime * speedY, distanceY)), transform.position.z);
        }
        /*
        if (speedX > 0 && distanceX > 0)
        {
            transform.position = new Vector3((_transform.position.x + Mathf.PingPong(Time.deltaTime * speedX, distanceX)), transform.position.y, transform.position.z);
        }
        else if(speedY > 0 && distanceY >0)
        {
            transform.position = new Vector3(transform.position.x, (_transform.position.y + Mathf.PingPong(Time.deltaTime * speedY, distanceY)), transform.position.z);
        }*/
    }
    public float GetSpeedX()
    {
        return speedX;
    }
}
