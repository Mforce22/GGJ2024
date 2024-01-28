using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : Singleton<GameMaster>, ISystem
{
    [SerializeField]
    private int _Priority;

    [SerializeField]
    private float secondsToWait;
    public int Priority { get => _Priority; }

    [Header("Player")]
    [SerializeField]
    private GameObject playerPrefab;


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

    [SerializeField]
    private GameEvent startMusicEvent;

    [SerializeField]
    private GameEvent assembleMusicEvent;

    [SerializeField]
    private GameEvent citySoundsEvent;

    [SerializeField]
    private GameEvent hintSoundEvent;

    [SerializeField]
    private GameEvent bipSoundEvent;

    [SerializeField]
    private GameEvent city1SoundEvent;


    private CameraController camera;

    private float cameraFloat;

    private int completedLevel = 0;

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
        AudioSystem.Instance.StartMusic();
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
        //Debug.Log("Match Won");

        //change image

        //change level
        completedLevel++;
        NextLevel();
        StartCoroutine(startGame(cameraFloat * 2));
    }

    private void LoseMatch(GameEvent evt)
    {
        Debug.Log("Match lost");
        StartCoroutine(lostGame(cameraFloat));


    }

    private void StartStory(GameEvent evt)
    {
        citySoundsEvent.Invoke();
        Debug.Log("Story started");

        //camera = GameObject.Find("Main Camera").GetComponent<CameraController>();

        //get a component in the scene
        camera = FindObjectOfType<CameraController>();
        cameraFloat = camera.GetDelayTime();

        StartCoroutine(waitSecsHint(waitTime));

        StartCoroutine(waitSecsAssemble(waitTime * 2));
        //camera.NextTarget();
        StartCoroutine(waitSecsBip(waitTime * 3));
        //camera.NextTarget();
        StartCoroutine(waitSecsCity1(waitTime * 4));
        //camera.NextTarget();
        StartCoroutine(startGame(waitTime * 4));
        //Start the game

    }
    private IEnumerator waitSecsCity1(float secs)
    {
        yield return new WaitForSeconds(secs);
        SoundSystem.Instance.SS_StopMusic();
        
        Debug.Log("Coroutine finished");
        camera.NextTarget();
        yield return new WaitForSeconds(secondsToWait);
        city1SoundEvent.Invoke();
    }
    private IEnumerator waitSecsBip(float secs)
    {
        yield return new WaitForSeconds(secs);
        SoundSystem.Instance.SS_StopMusic();
        
        Debug.Log("Coroutine finished");
        camera.NextTarget();
        yield return new WaitForSeconds(secondsToWait);
        bipSoundEvent.Invoke();
    }
    private IEnumerator waitSecsHint(float secs)
    {
        yield return new WaitForSeconds(secs);
        SoundSystem.Instance.SS_StopMusic();

        Debug.Log("Coroutine finished");
        camera.NextTarget();
        yield return new WaitForSeconds(secondsToWait);
        hintSoundEvent.Invoke();
    }
    private IEnumerator waitSecsAssemble(float secs)
    {
        yield return new WaitForSeconds(secs);
        SoundSystem.Instance.SS_StopMusic();
        
        Debug.Log("Coroutine finished");
        camera.NextTarget();
        yield return new WaitForSeconds(secondsToWait);
        assembleMusicEvent.Invoke();
    }

    private IEnumerator startGame(float secs)
    {
        yield return new WaitForSeconds(secs);
        NextLevel();
        yield return new WaitForSeconds(cameraFloat);
        NextLevel();
        yield return new WaitForSeconds(cameraFloat);
        //spawn player
        SpawnPlayer();
    }

    private IEnumerator lostGame(float secs)
    {
        PreviousLevel();
        yield return new WaitForSeconds(secs);
        NextLevel();
        yield return new WaitForSeconds(secs);
        //spawn player
        SpawnPlayer();
    }

    void NextLevel()
    {
        camera.NextTarget();
    }

    void PreviousLevel()
    {
        Debug.Log("Previous Level");
        camera.PreviousTarget();
    }

    void SpawnPlayer()
    {
        //spawn the player

        GameObject spawnLocation = camera.GetPlayerSpawnPoint(completedLevel);

        //instantiate player
        if (spawnLocation != null)
        {
            Instantiate(playerPrefab, spawnLocation.transform.position, Quaternion.identity);
        }
    }
}
