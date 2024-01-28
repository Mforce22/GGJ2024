using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [System.Serializable]
    private struct Target
    {
        public GameObject obj;
    }

    private int index = 0;
    [SerializeField]
    private Target[] targets;
    [SerializeField]
    private float time = 2f;

    [Header("Player Spawn")]
    [SerializeField]
    private GameObject[] playerSpawnPoints;


    [Header("Events")]
    [SerializeField]
    private GameEvent startStoryEvent;

    [Header("City Images")]
    [SerializeField]
    private Image cityImage1;

    [SerializeField]
    private Image cityImage2;

    [SerializeField]
    private Image cityImage3;

    [Header("Sprites")]
    [SerializeField]
    private Sprite city1;

    [SerializeField]
    private Sprite city2;

    [SerializeField]
    private Sprite city3;

    private bool isLerping = false;

    //private GameMaster gameMaster;

    // Start is called before the first frame update
    void Start()
    {
        // set the camera to the first target
        transform.position = targets[index].obj.transform.position;

        //get game master with instance
        //gameMaster = GameMaster.Instance;

        

    }

    private void OnEnable()
    {
        Debug.Log("Emilio Badabodele");
        startStoryEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        //if space is pressed, NextTarget is called
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    NextTarget();
        //}

        if (isLerping)
        {
            if (Vector3.Distance(transform.position, targets[index].obj.transform.position) < 0.1f)
            {
                isLerping = false;
            }
            transform.position = Vector3.Lerp(transform.position, targets[index].obj.transform.position, time * Time.deltaTime);
        }
    }

    public void NextTarget()
    {
        // slowly lerp to the next target using transform.position
        index++;
        if (index >= targets.Length)
        {
            index = 0;
        }
        isLerping = true;
    }

    public void PreviousTarget()
    {
        // slowly lerp to the previous target
        index--;
        if (index < 0)
        {
            index = targets.Length - 1;
        }
        isLerping = true;
    }

    public float GetDelayTime()
    {
        return time;
    }

    public GameObject GetPlayerSpawnPoint(int index)
    {
        return playerSpawnPoints[index];
    }

    public void setCity(int index)
    {
        if(index == 0)
        {
            //cityImage1.set
            //set sprite of city 1
            cityImage1.sprite = city1;
        }
        if(index == 1)
        {
            cityImage1.sprite = city2;
        }
        if( index == 2)
        {
            cityImage1.sprite = city3;
        }
    }
}