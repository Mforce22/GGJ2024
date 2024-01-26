using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isLerping = false;

    // Start is called before the first frame update
    void Start()
    {
        // set the camera to the first target
        transform.position = targets[index].obj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if space is pressed, NextTarget is called
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     NextTarget();
        // }

        if (isLerping)
        {
            if (Vector3.Distance(transform.position, targets[index].obj.transform.position) < 0.1f)
            {
                isLerping = false;
            }
            transform.position = Vector3.Lerp(transform.position, targets[index].obj.transform.position, time * Time.deltaTime);
        }
    }

    void NextTarget()
    {
        // slowly lerp to the next target using transform.position
        index++;
        if (index >= targets.Length)
        {
            index = 0;
        }
        isLerping = true;
    }

    void PreviousTarget()
    {
        // slowly lerp to the previous target
        index--;
        if (index < 0)
        {
            index = targets.Length - 1;
        }
        isLerping = true;
    }
}