using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonHandler : MonoBehaviour
{
    public Toggle fullscreen;
    public Slider brightness;
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider sfxVolume;
    public Button controllers;
    public Button back;

    private void Awake()
    {
        fullscreen.onValueChanged.AddListener(OnFullscreenToggle);
        brightness.onValueChanged.AddListener(OnBrightnessChange);
        masterVolume.onValueChanged.AddListener(OnMasterVolumeChange);
        musicVolume.onValueChanged.AddListener(OnMusicVolumeChange);
        sfxVolume.onValueChanged.AddListener(OnSFXVolumeChange);
        controllers.onClick.AddListener(OnControllers);
        back.onClick.AddListener(OnBack);
    }

    private void OnFullscreenToggle(bool toggled)
    {
        throw new NotImplementedException();
    }

    private void OnBrightnessChange(float value)
    {
        throw new NotImplementedException();
    }
    
    private void OnMasterVolumeChange(float value)
    {
        throw new NotImplementedException();
    }
    
    private void OnMusicVolumeChange(float value)
    {
        throw new NotImplementedException();
    }
    
    private void OnSFXVolumeChange(float value)
    {
        throw new NotImplementedException();
    }

    private void OnControllers()
    {
        throw new NotImplementedException();
    }

    private void OnBack()
    {
        throw new NotImplementedException();
    }
}
