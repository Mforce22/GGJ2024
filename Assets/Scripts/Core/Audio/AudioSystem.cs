using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This class manages the audio system in the game, controlling music playback and volume.
/// It inherits from Singleton<AudioSystem> and implements ISystem.
/// </summary>
public class AudioSystem : Singleton<AudioSystem>, ISystem {
    [SerializeField]
    private int _Priority; // Priority of the audio system.
    public int Priority { get => _Priority; } // Property to access the priority.

    private AudioSource musicSource; // AudioSource component for playing music.

    /// <summary>
    /// Sets up the audio system by configuring the AudioSource component.
    /// </summary>
    public void Setup() {
        musicSource = GetComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = 0.8f; // Default volume.
        

        // Signal that the system setup is finished.
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }

    /// <summary>
    /// Sets the volume of the music.
    /// </summary>
    /// <param name="volume">The desired volume value.</param>
    public void SetVolume(float volume) {
        musicSource.volume = volume;
    }

    /// <summary>
    /// Starts playing the music.
    /// </summary>
    public void StartMusic() {
        musicSource.Play();
    }

    /// <summary>
    /// Stops playing the music.
    /// </summary>
    public void StopMusic() {
        musicSource.Stop();
    }

    public float GetVolume()
    {
        return musicSource.volume;
    }
}