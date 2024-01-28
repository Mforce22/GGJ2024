using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private int numberGasCanister = 5;

    [Header("Events")]

    [SerializeField]
    private GameEvent DeathEvent2;

    [SerializeField]
    private GameEvent WinEvent;

    [SerializeField]
    private GameEvent LoseEvent;

    [SerializeField]
    private Transform arm1;
    [SerializeField]
    private Transform arm2;
    [SerializeField]
    private Transform body;

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
            Die();
        }
    }

    private void Die()
    {
        arm1.transform.SetParent(null);
        arm2.transform.SetParent(null);
        arm1.GetComponent<Rigidbody>().isKinematic = false;
        arm2.GetComponent<Rigidbody>().isKinematic = false;
        body.transform.SetParent(null);
        body.GetComponent<Rigidbody>().isKinematic = false;
        body.GetComponent<BoxCollider>().enabled = true;
        // add forces
        float deathForce = 300;
        float leftArmRotation = Random.Range(-1f, 1f);
        float rightArmRotation = Random.Range(-1f, 1f);
        float leftArmForceX = Random.Range(-1f, 1f);
        float rightArmForceX = Random.Range(-1f, 1f);
        float leftArmForceY = Random.Range(-1f, 1f);
        float rightArmForceY = Random.Range(-1f, 1f);
        arm1.GetComponent<Rigidbody>().AddForce(new Vector3(leftArmForceX, leftArmForceY, -1) * deathForce / 2);
        arm2.GetComponent<Rigidbody>().AddForce(new Vector3(rightArmForceX, rightArmForceY, -1) * deathForce / 2);
        body.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1) * deathForce / 2);
        arm1.GetComponent<Rigidbody>().AddTorque(new Vector3(0, leftArmRotation, -1) * deathForce);
        arm2.GetComponent<Rigidbody>().AddTorque(new Vector3(0, rightArmRotation, -1) * deathForce);
        body.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -1) * deathForce);
        StartCoroutine(PlayerLost());
    }

    private void Update()
    {
        //get transform
        if (transform.position.y <= -5)
        {
            //tartCoroutine(PlayerLost());
            //Destroy(gameObject);
            //set location to 1000

            transform.position = new Vector3(1000, 1000, 1000);
            Die();
        }
    }

    private IEnumerator PlayerLost()
    {
        yield return new WaitForSeconds(2f);
        LoseEvent.Invoke();
        DeathEvent2.Invoke();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
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

    public void PlayerDeath()
    {
        Destroy(gameObject);
        StartCoroutine(PlayerLost());
    }
}
