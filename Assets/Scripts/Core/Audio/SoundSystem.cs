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
    private GameEvent citySound1Event;

    [SerializeField]
    private GameEvent citySound2Event;

    [SerializeField]
    private GameEvent completedLevelEvent;

    [SerializeField]
    private GameEvent ideaDingEvent;

    [SerializeField]
    private GameEvent weldEvent;

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

    [SerializeField]
    private GameEvent scientistVoiceEvent;

    [SerializeField]
    private GameEvent crowdSad1Event;

    [SerializeField]
    private GameEvent crowdSad2Event;

    [SerializeField]
    private GameEvent crowdSad3Event;

    [SerializeField]
    private GameEvent crowdSad4Event;

    [SerializeField]
    private GameEvent crowdSad5Event;

    [SerializeField]
    private GameEvent crowdSad6Event;

    [SerializeField]
    private GameEvent crowdSad7Event;

    [SerializeField]
    private GameEvent crowdSad8Event;

    [SerializeField]
    private GameEvent crowdLaugh1Event;

    [SerializeField]
    private GameEvent crowdLaugh2Event;

    [SerializeField]
    private GameEvent crowdLaugh3Event;

    [SerializeField]
    private GameEvent backgroundMusicEvent;


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

    [SerializeField]
    private AudioClip citySound1;

    [SerializeField]
    private AudioClip citySound2;

    [Header("Game Sounds")]
    [SerializeField]
    private AudioClip completedLevel;

    [SerializeField]
    private AudioClip ideaDing;

    [SerializeField]
    private AudioClip weld;

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

    [Header("Voices")]

    [SerializeField]
    private AudioClip scientistVoice;

    [SerializeField]
    private AudioClip crowdSad1;

    [SerializeField]
    private AudioClip crowdSad2;

    [SerializeField]
    private AudioClip crowdSad3;

    [SerializeField]
    private AudioClip crowdSad4;

    [SerializeField]
    private AudioClip crowdSad5;

    [SerializeField]
    private AudioClip crowdSad6;

    [SerializeField]
    private AudioClip crowdSad7;

    [SerializeField]
    private AudioClip crowdSad8;

    [SerializeField]
    private AudioClip crowdLaugh1;

    [SerializeField]
    private AudioClip crowdLaugh2;

    [SerializeField]
    private AudioClip crowdLaugh3;

    [Header("Background Music")]
    [SerializeField]
    private AudioClip backgroundMusic;

    #endregion

    public void Setup()
    {
        batWingsEvent.Subscribe(BatWings);
        ratSqueakEvent.Subscribe(RatSqueak);
        rainDropsEvent.Subscribe(RainDrops);
        citySound1Event.Subscribe(CitySound1);
        citySound2Event.Subscribe(CitySound2);
        completedLevelEvent.Subscribe(CompletedLevel);
        ideaDingEvent.Subscribe(IdeaDing);
        weldEvent.Subscribe(Weld);
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
        scientistVoiceEvent.Subscribe(ScientistVoice);

        crowdSad1Event.Subscribe(CrowdSad1);
        crowdSad2Event.Subscribe(CrowdSad2);
        crowdSad3Event.Subscribe(CrowdSad3);
        crowdSad4Event.Subscribe(CrowdSad4);
        crowdSad5Event.Subscribe(CrowdSad5);
        crowdSad6Event.Subscribe(CrowdSad6);
        crowdSad7Event.Subscribe(CrowdSad7);
        crowdSad8Event.Subscribe(CrowdSad8);

        crowdLaugh1Event.Subscribe(CrowdLaugh1);
        crowdLaugh2Event.Subscribe(CrowdLaugh2);
        crowdLaugh3Event.Subscribe(CrowdLaugh3);

        backgroundMusicEvent.Subscribe(BackgroundMusic);


        musicSource = GetComponent<AudioSource>();
        musicSource.loop = false;
        musicSource.playOnAwake = false;
        musicSource.volume = 0.6f; // Default volume.
        // Signal that the system setup is finished.
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }
    private void OnDisable()
    {
        batWingsEvent.Unsubscribe(BatWings);
        ratSqueakEvent.Unsubscribe(RatSqueak);
        rainDropsEvent.Unsubscribe(RainDrops);
        citySound1Event.Unsubscribe(CitySound1);
        citySound2Event.Unsubscribe(CitySound2);
        completedLevelEvent.Unsubscribe(CompletedLevel);
        ideaDingEvent.Unsubscribe(IdeaDing);
        weldEvent.Unsubscribe(Weld);
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
        scientistVoiceEvent.Unsubscribe(ScientistVoice);

        crowdSad1Event.Unsubscribe(CrowdSad1);
        crowdSad2Event.Unsubscribe(CrowdSad2);
        crowdSad3Event.Unsubscribe(CrowdSad3);
        crowdSad4Event.Unsubscribe(CrowdSad4);
        crowdSad5Event.Unsubscribe(CrowdSad5);
        crowdSad6Event.Unsubscribe(CrowdSad6);
        crowdSad7Event.Unsubscribe(CrowdSad7);
        crowdSad8Event.Unsubscribe(CrowdSad8);

        crowdLaugh1Event.Unsubscribe(CrowdLaugh1);
        crowdLaugh2Event.Unsubscribe(CrowdLaugh2);
        crowdLaugh3Event.Unsubscribe(CrowdLaugh3);

        backgroundMusicEvent.Unsubscribe(BackgroundMusic);

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

    void CitySound1(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = citySound1;
        musicSource.Play();
    }

    void CitySound2(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = citySound2;
        musicSource.Play();
    }
    void CompletedLevel(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = completedLevel;
        musicSource.Play();
    }

    void IdeaDing(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = ideaDing;
        musicSource.Play();
    }

    void Weld(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = weld;
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
    void ScientistVoice(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = scientistVoice;
        musicSource.Play();
    }

    void CrowdSad1(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdSad1;
        musicSource.Play();
    }

    void CrowdSad2(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdSad2;
        musicSource.Play();
    }

    void CrowdSad3(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdSad3;
        musicSource.Play();
    }

    void CrowdSad4(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdSad4;
        musicSource.Play();
    }

    void CrowdSad5(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdSad5;
        musicSource.Play();
    }

    void CrowdSad6(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdSad6;
        musicSource.Play();
    }

    void CrowdSad7(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdSad7;
        musicSource.Play();
    }

    void CrowdSad8(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdSad8;
        musicSource.Play();
    }

    void CrowdLaugh1(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdLaugh1;
        musicSource.Play();
    }

    void CrowdLaugh2(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdLaugh2;
        musicSource.Play();
    }

    void CrowdLaugh3(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = crowdLaugh3;
        musicSource.Play();
    }

    void BackgroundMusic(GameEvent evt)
    {
        musicSource.Stop();
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public float GetVolume()
    {
        return musicSource.volume;}
    public void SS_StopMusic()
    {
        musicSource.Stop();
    }
}
