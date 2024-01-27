using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField]
    private int activeInLevel = 0;

    [SerializeField]
    private int spawnAfter = 10;

    [Tooltip("The minimum time between each spawn")]
    [SerializeField]
    private int minSpawnTime = 0;

    [Tooltip("The maximum time between each spawn")]
    [SerializeField]
    private int maxSpawnTime = 10;

    [Header("Events")]
    [SerializeField]
    private GameEvent changeLevelEvent;


    [Header("Limiters")]
    [SerializeField]
    private GameObject leftLimiters;

    [SerializeField]
    private GameObject rightLimiters;

    [Header("Falling Objects")]
    [SerializeField]
    private GameObject fallingObject;

    private bool isActivated = false;

    private float timeBeforeStart = 1000;

    private float actualTime = 1000;

    private int leftX = 0;

    private int rightX = 0;

    private void OnEnable()
    {
        //subscribe to event
        changeLevelEvent.Subscribe(ChangeLevelCallback);

        //set the timer
        timeBeforeStart = spawnAfter;
        SetTimer();

        //limiters
        leftX = (int)leftLimiters.transform.position.x;
        rightX = (int)rightLimiters.transform.position.x;

        //debug
        isActivated = true;
    }
    

    private void ChangeLevelCallback(GameEvent evt)
    {
        GameEvent gameEvent = (GameEvent)evt;
        if (gameEvent == changeLevelEvent)
        {
            if (activeInLevel == GameMaster.Instance.GetActiveLevel())
            {
                isActivated = true;
            }
            else
            {
                isActivated = false;
            }
        }
    }   

    // Update is called once per frame
    void Update()
    {
        if(isActivated)
        {
            if (timeBeforeStart <= 0)
            {
                if(actualTime <= 0)
                {
                    Spawn();
                }
                else
                {
                    actualTime -= Time.deltaTime;
                }
            }
            else 
            { 
                timeBeforeStart -= Time.deltaTime;
            }
        }
    }

    void Spawn()
    {
        //select a random number between leftX and rightX
        int randomX = Random.Range(leftX, rightX);

        Debug.Log("Spawn at location: " +  randomX);

        //spawn the object
        Instantiate(fallingObject, new Vector3(randomX, transform.position.y, transform.position.z), Quaternion.identity);

        SetTimer();
    }

    void SetTimer()
    {
        Debug.Log("Timer");
        actualTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
}



