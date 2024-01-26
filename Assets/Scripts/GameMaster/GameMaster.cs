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


    public void Setup()
    {
        SystemCoordinator.Instance.FinishSystemSetup(this);
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

    public void SetActiveLevel(int level) {  
        activeLevel = level;
        changeLevelEvent.Invoke();
    }
}
