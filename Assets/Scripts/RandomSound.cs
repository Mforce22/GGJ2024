using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [SerializeField]
    private GameEvent soundEvent;

    [SerializeField]
    private float minTime = 5f;

    [SerializeField]
    private float maxTime = 10f;

    private void Start()
    {
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            soundEvent.Invoke();
        }
    }
}
