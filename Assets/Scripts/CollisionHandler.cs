using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private int numberGasCanister = 10;

    [Header("Events")]

    [SerializeField]
    private GameEvent WinEvent;

    [SerializeField]
    private GameEvent LoseEvent;

    private int gasTaken = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Gas"))
        {
            Debug.Log("GAS");
            Destroy(other.gameObject);
            gasTaken++;
            if (gasTaken >= numberGasCanister)
            {
                PlayerWon();
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY");
            // self destruct
            Destroy(gameObject);
            StartCoroutine(PlayerLost());
        }
    }

    private void Update()
    {
        //get transform
        if (transform.position.y <= -5)
        {
            StartCoroutine(PlayerLost());
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayerLost()
    {
        LoseEvent.Invoke();
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }

    private void PlayerWon()
    {
        WinEvent.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlatformMovement platform = collision.gameObject.GetComponent<PlatformMovement>();

        if (platform != null)
        {
            if (platform.GetSpeedX() != 0 && transform.position.y > platform.transform.position.y)
            {
                transform.SetParent(platform.gameObject.transform);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        PlatformMovement platform = collision.gameObject.GetComponent<PlatformMovement>();

        if (platform != null)
        {
            if (platform.GetSpeedX() != 0)
            {
                transform.SetParent(null);
            }
        }
    }
}
