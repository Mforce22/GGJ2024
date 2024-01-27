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
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Gas"))
        {
            Debug.Log("GAS");
            Destroy(other.gameObject);
            gasTaken++;
            if (gasTaken >= numberGasCanister)
            {
                WinEvent.Invoke();
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY");
            // self destruct
            Destroy(gameObject);
            LoseEvent.Invoke();
        }
    }

    private void PlayerLost()
    {
        LoseEvent.Invoke();
    }

    private void PlayerWon()
    {
        WinEvent.Invoke();
    }
}
