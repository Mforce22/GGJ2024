using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        // Set the collider as trigger
        BoxCollider collider = GetComponent<BoxCollider>();

        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
