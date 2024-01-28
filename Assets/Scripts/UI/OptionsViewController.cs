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
        //audioSlider.value = AudioSystem.Instance.GetVolume();
        soundSlider.value = SoundSystem.Instance.GetVolume();
    }

    public void setAudioVolume()
    {
        //AudioSystem.Instance.SetVolume(volume);
    }

    public void setSoundVolume(float volume)
    {
        SoundSystem.Instance.SetVolume(volume);
    }
}
