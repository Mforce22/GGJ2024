using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewController : MonoBehaviour
{
    [SerializeField]
    private OptionsViewController _OptionsViewPrefab;
    [SerializeField]
    private CreditsViewController _CreditsViewPrefab;

    private OptionsViewController _optionsViewController;
    private CreditsViewController _creditsViewController;

    public void ChangeScene(string scene)
    {
        TravelSystem.Instance.SceneLoad(scene);
    }

    public void OpenOptions()
    {
        if (_optionsViewController) return;
        _optionsViewController = Instantiate(_OptionsViewPrefab);
    }
    public void OpenCredits()
    {
        if (_creditsViewController) return;
        _creditsViewController = Instantiate(_CreditsViewPrefab);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
