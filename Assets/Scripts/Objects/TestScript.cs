using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Sono entrato");

        Obstacle obs = other.GetComponent<Obstacle>();

        if (obs != null)
        {
            Debug.Log("Sono stato colpito");
        }
    }
}
