using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Gas"))
        {
            Debug.Log("GAS");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY");
            // self destruct
            Destroy(gameObject);
        }
    }
}
