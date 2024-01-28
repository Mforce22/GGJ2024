using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsViewController : MonoBehaviour
{
    [SerializeField]
    private Slider audioSlider;

    [SerializeField]
    private Slider soundSlider;

    public void CloseOption()
    {
        Destroy(gameObject);
    }
    private void Start()
    {
        audioSlider.value = AudioSystem.Instance.GetVolume();
        soundSlider.value = SoundSystem.Instance.GetVolume();
    }

    public void setAudioVolume()
    {
        AudioSystem.Instance.SetVolume(audioSlider.value);
        audioSlider.value = AudioSystem.Instance.GetVolume();
    }

    public void setSoundVolume()
    {
        SoundSystem.Instance.SetVolume(soundSlider.value);
        soundSlider.value = SoundSystem.Instance.GetVolume();
    }
}
