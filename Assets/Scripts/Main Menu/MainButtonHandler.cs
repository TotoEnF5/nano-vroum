using System;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonHandler : MonoBehaviour
{
    public Button play;
    public Button settings;
    public Button credits;
    public Button quit;

    private void Awake()
    {
        play.onClick.AddListener(OnPlay);
        settings.onClick.AddListener(OnSettings);
        credits.onClick.AddListener(OnCredits);
        quit.onClick.AddListener(OnQuit);
    }

    private void OnPlay()
    {
        throw new NotImplementedException();
    }
    
    private void OnSettings()
    {
        throw new NotImplementedException();
    }
    
    private void OnCredits()
    {
        throw new NotImplementedException();
    }
    
    private void OnQuit()
    {
        Application.Quit();
    }
}
