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

    [Header("Events")]
    [SerializeField]
    private GameEvent changeLevelEvent;

    [SerializeField]
    private GameEvent winMatchEvent;

    [SerializeField]
    private GameEvent loseMatchEvent;


    public void Setup()
    {
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    private void OnEnable()
    {
        winMatchEvent.Subscribe(WinMatch);
        loseMatchEvent.Subscribe(LoseMatch);
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
}
