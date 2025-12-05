using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButtonHandler : MonoBehaviour
{
    public Button backToGame;
    public Button settings;
    public Button backToMainMenu;
    public Button quitGame;

    public void Awake()
    {
        backToGame.onClick.AddListener(OnBackToGame);
        settings.onClick.AddListener(OnSettings);
        backToMainMenu.onClick.AddListener(OnBackToMainMenu);
        quitGame.onClick.AddListener(OnQuitGame);
    }
    
    private void OnBackToGame()
    {
        throw new NotImplementedException();
    }
    
    private void OnSettings()
    {
        SceneManager.LoadScene("MenuSettings", LoadSceneMode.Additive);
    }
    
    private void OnBackToMainMenu()
    {
        throw new NotImplementedException();
    }
    
    private void OnQuitGame()
    {
        Application.Quit();
    }
}
