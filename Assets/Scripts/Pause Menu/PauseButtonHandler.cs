using System;
using UnityEngine;
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
        throw new NotImplementedException();
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
