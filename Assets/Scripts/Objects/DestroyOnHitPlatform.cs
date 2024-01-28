using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHitPlatform : MonoBehaviour
{

    float timePassed = 0;

    bool canCollide = false;

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 0.5f && !canCollide)
        {
            canCollide = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Prova collisione");
        Platform platform = collision.gameObject.GetComponent<Platform>();

        if (platform != null && canCollide)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Prova trigger");
        Platform platform = other.gameObject.GetComponent<Platform>();

        if (platform != null && canCollide)
        {
            Destroy(gameObject);
        }

        CharacterController player = other.gameObject.GetComponent<CharacterController>();

        if (player != null)
        {
            player.GetComponent<CollisionHandler>().PlayerDeath();
            Destroy(gameObject);

        }
    }

}
