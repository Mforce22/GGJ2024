using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour
{
    [SerializeField]
    private float timeToDissapear;
    [SerializeField]
    private float timeToReappear;
    private void Awake()
    {
        StartCoroutine(Dissapear());
    }

    IEnumerator Dissapear()
    {
        while (true) {
            yield return new WaitForSeconds(timeToDissapear);

            for (int i = 0; i < 10; i++)
            {
                //gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                //gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                yield return new WaitForSeconds(0.1f);
            }

            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;


            yield return new WaitForSeconds(timeToReappear);

            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
