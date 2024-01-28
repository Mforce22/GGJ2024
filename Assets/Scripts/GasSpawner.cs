using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject gas;
    [SerializeField]
    private List<GameObject> gasSpawnPoints;
    private List<Vector3> gasTargets;
    [Header("Events")]
    [SerializeField]
    private GameEvent changeLevelEvent;
    [SerializeField]
    private int activeInLevel = 0;

    private void OnEnable()
    {
        //subscribe to event
        changeLevelEvent.Subscribe(ChangeLevelCallback);
    }

    // Start is called before the first frame update
    void Activate()
    {
        reset();
        InvokeRepeating("SpawnNew", 0.0f, 5.0f);
    }

    void Deactivate()
    {
        CancelInvoke();
    }

    private void ChangeLevelCallback(GameEvent evt)
    {


        if (activeInLevel == GameMaster.Instance.GetActiveLevel())
        {
            Activate();
        }
        else
        {
            Deactivate();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    void reset()
    {
        // delete all actors with the tag "Gas"
        GameObject[] gasObjects = GameObject.FindGameObjectsWithTag("Gas");
        for (int i = 0; i < gasObjects.Length; i++)
        {
            Destroy(gasObjects[i]);
        }
        gasTargets = new List<Vector3>();
        for (int i = 0; i < gasSpawnPoints.Count; i++)
        {
            gasTargets.Add(gasSpawnPoints[i].transform.position);
        }
    }

    void SpawnNew()
    {
        // if there is at least 1 gas in the scene, do nothing
        if (GameObject.FindGameObjectsWithTag("Gas").Length > 0)
        {
            return;
        }
        if (gasTargets.Count == 0)
        {
            reset();
        }
        // randomly select a spawn point
        int index = Random.Range(0, gasTargets.Count);
        // spawn a new gas at that point
        Vector3 location = gasTargets[index];
        GameObject newGas = Instantiate(gas, location, Quaternion.identity);
        newGas.tag = "Gas";
        // remove that spawn point from the list
        gasTargets.RemoveAt(index);
    }
}
