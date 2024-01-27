using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : Singleton<GameMaster>, ISystem
{
    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }

    [Header("Variables")]
    [SerializeField]
    private int activeLevel = 0;

    [SerializeField]
    private float waitTime = 3f;

    [Header("Events")]
    [SerializeField]
    private GameEvent changeLevelEvent;

    [SerializeField]
    private GameEvent startStoryEvent;

    [SerializeField]
    private GameEvent winMatchEvent;

    [SerializeField]
    private GameEvent loseMatchEvent;


    private CameraController camera;

    public void Setup()
    {
        winMatchEvent.Subscribe(WinMatch);
        loseMatchEvent.Subscribe(LoseMatch);
        startStoryEvent.Subscribe(StartStory);
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    private void OnDisable()
    {
        winMatchEvent.Unsubscribe(WinMatch);
        loseMatchEvent.Unsubscribe(LoseMatch);
        startStoryEvent.Unsubscribe(StartStory);
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetActiveLevel()
    {
        return activeLevel;
    }

    public void SetActiveLevel(int level)
    {
        activeLevel = level;
        changeLevelEvent.Invoke();
    }

    private void WinMatch(GameEvent evt)
    {
        Debug.Log("Match Won");
    }

    private void LoseMatch(GameEvent evt)
    {
        Debug.Log("Match lost");
    }

    private void StartStory(GameEvent evt)
    {
        Debug.Log("Story started");

        //camera = GameObject.Find("Main Camera").GetComponent<CameraController>();

        //get a component in the scene
        camera = FindObjectOfType<CameraController>();

        StartCoroutine(waitSecs(waitTime));
        StartCoroutine(waitSecs(waitTime * 2));
        //camera.NextTarget();
        StartCoroutine(waitSecs(waitTime * 3));
        //camera.NextTarget();
        StartCoroutine(waitSecs(waitTime * 4));
        //camera.NextTarget();
    }

    private IEnumerator waitSecs(float secs)
    {
        yield return new WaitForSeconds(secs);
        Debug.Log("Coroutine finished");
        camera.NextTarget();
    }
}
