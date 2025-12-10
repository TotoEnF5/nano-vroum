using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;

public class MainButtonHandler : MonoBehaviour
{
    public LoadingScript loadingScript;

    public GameObject creditsLayer;
    
    public Button play;
    public Button settings;
    public Button credits;
    public Button quit;
    public Button creditsBack;

    private void Awake()
    {
        play.onClick.AddListener(OnPlay);
        settings.onClick.AddListener(OnSettings);
        credits.onClick.AddListener(OnCredits);
        quit.onClick.AddListener(OnQuit);
        creditsBack.onClick.AddListener(OnCreditsBack);
    }

    private void OnPlay()
    {
        loadingScript.LoadScene("Level");
        // SceneManager.LoadScene("Level");
    }
    
    private void OnSettings()
    {
        SceneManager.LoadScene("MenuSettings", LoadSceneMode.Additive);
    }
    
    private void OnCredits()
    {
        creditsLayer.SetActive(true);
    }
    
    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnCreditsBack()
    {
        creditsLayer.SetActive(false);
    }

}
