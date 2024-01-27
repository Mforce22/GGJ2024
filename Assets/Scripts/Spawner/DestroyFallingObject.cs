using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFallingObject : MonoBehaviour
{

    [SerializeField]
    private int lifeTime = 0;


    private float actualLifeTime = 0;
    private void OnEnable()
    {
        actualLifeTime = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        actualLifeTime -= Time.deltaTime;
        if(actualLifeTime <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Sono stato terminato");
        }
    }
}
