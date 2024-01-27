using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>, ISystem
{
    [SerializeField]
    private int _Priority; // Priority of the audio system.
    public int Priority { get => _Priority; }

    private AudioSource musicSource; // AudioSource component for playing music.

    #region GameEvents
    [Header("Game Events")]

    [SerializeField]
    private GameEvent batWingsEvent;

    [SerializeField]
    private GameEvent ratSqueakEvent;

    [SerializeField]
    private GameEvent rainDropsEvent;

    [SerializeField]
    private GameEvent completedLevelEvent;

    [SerializeField]
    private GameEvent accelerationEvent;

    [SerializeField]
    private GameEvent constantMovementEvent;

    [SerializeField]
    private GameEvent decelerationEvent;

    [SerializeField]
    private GameEvent jumpEvent;

    [SerializeField]
    private GameEvent descendingEvent;

    [SerializeField]
    private GameEvent talkEvent;

    [SerializeField]
    private GameEvent dieEvent;

    [SerializeField]
    private GameEvent obtainGasEvent;

    [SerializeField]
    private GameEvent beep1Event;

    [SerializeField]
    private GameEvent beep2Event;

    [SerializeField]
    private GameEvent beep3Event;

    #endregion

    #region Clips
    [Header("Clips")]

    [Header("Animal")]

    [SerializeField]
    private AudioClip batWings;


    [SerializeField]
    private AudioClip ratSqueak;

    [Header("Environment")]
    [SerializeField]
    private AudioClip rainDrops;

    [Header("Game Sounds")]
    [SerializeField]
    private AudioClip completedLevel;

    [Header("Robot")]
    [SerializeField]
    private AudioClip accelerationRobot;

    [SerializeField]
    private AudioClip constantMovementRobot;

    [SerializeField]
    private AudioClip decelerationRobot;

    [SerializeField]
    private AudioClip jumpRobot;

    [SerializeField]
    private AudioClip descendingRobot;

    [SerializeField]
    private AudioClip talkRobot;

    [SerializeField]
    private AudioClip dieRobot;

    [SerializeField]
    private AudioClip obtainGasRobot;

    [SerializeField]
    private AudioClip beep1Robot;
    [SerializeField]

    private AudioClip beep2Robot;
    [SerializeField]

    private AudioClip beep3Robot;
    #endregion

    public void Setup()
    {
        batWingsEvent.Subscribe(BatWings);
        ratSqueakEvent.Subscribe(RatSqueak);
        rainDropsEvent.Subscribe(RainDrops);
        completedLevelEvent.Subscribe(CompletedLevel);
        accelerationEvent.Subscribe(Acceleration);
        constantMovementEvent.Subscribe(ConstantMovement);
        decelerationEvent.Subscribe(Deceleration);
        jumpEvent.Subscribe(Jump);
        descendingEvent.Subscribe(Descending);
        talkEvent.Subscribe(Talk);
        dieEvent.Subscribe(Die);
        obtainGasEvent.Subscribe(ObtainGas);
        beep1Event.Subscribe(Beep1);
        beep2Event.Subscribe(Beep2);
        beep3Event.Subscribe(Beep3);

        musicSource = GetComponent<AudioSource>();
        musicSource.loop = false;
        musicSource.playOnAwake = false;
        musicSource.volume = 0.5f; // Default volume.
        // Signal that the system setup is finished.
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }
    private void OnDisable()
    {
        batWingsEvent.Unsubscribe(BatWings);
        ratSqueakEvent.Unsubscribe(RatSqueak);
        rainDropsEvent.Unsubscribe(RainDrops);
        completedLevelEvent.Unsubscribe(CompletedLevel);
        accelerationEvent.Unsubscribe(Acceleration);
        constantMovementEvent.Unsubscribe(ConstantMovement);
        decelerationEvent.Unsubscribe(Deceleration);
        jumpEvent.Unsubscribe(Jump);
        descendingEvent.Unsubscribe(Descending);
        talkEvent.Unsubscribe(Talk);
        dieEvent.Unsubscribe(Die);
        obtainGasEvent.Unsubscribe(ObtainGas);
        beep1Event.Unsubscribe(Beep1);
        beep2Event.Unsubscribe(Beep2);
        beep3Event.Unsubscribe(Beep3);



    }

    void BatWings(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = batWings;
        musicSource.Play();
    }

    void RatSqueak(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = ratSqueak;
        musicSource.Play();
    }
    void RainDrops(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = rainDrops;
        musicSource.Play();
    }
    void CompletedLevel(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = completedLevel;
        musicSource.Play();
    }
    void Acceleration(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = accelerationRobot;
        musicSource.Play();
    }
    void ConstantMovement(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = constantMovementRobot;
        musicSource.Play();
    }
    void Deceleration(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = decelerationRobot;
        musicSource.Play();
    }
    void Jump(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = jumpRobot;
        musicSource.Play();
    }
    void Descending(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = descendingRobot;
        musicSource.Play();
    }
    void Talk(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = talkRobot;
        musicSource.Play();
    }

    void Die(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = dieRobot;
        musicSource.Play();
    }

    void ObtainGas(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = obtainGasRobot;
        musicSource.Play();
    }
    void Beep1(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = beep1Robot;
        musicSource.Play();
    }
    void Beep2(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = beep2Robot;
        musicSource.Play();
    }
    void Beep3(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = beep3Robot;
        musicSource.Play();
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
