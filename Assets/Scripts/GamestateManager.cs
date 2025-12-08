using System;
using DG.Tweening;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum Gamestate
{
    Playing,
    Paused,
    GameOver,
}

public class GamestateManager : MonoBehaviour
{
    public static GamestateManager Instance { get; private set; }
    
    public Transform Character;
    public MoveCamera Camera;
    
    private Transform _currentCheckpoint;
    private Gamestate _gamestate = Gamestate.Playing;
    
    private InputAction _pauseAction;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        _pauseAction = InputSystem.actions.FindAction("Pause");
    }
    
    private void Update()
    {
        if (_pauseAction.triggered)
        {
            Scene pauseScene = SceneManager.GetSceneByName("MenuPause");
            if (pauseScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(pauseScene);
            }
            else
            {
                SceneManager.LoadScene("MenuPause", LoadSceneMode.Additive);
            }
        }
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        _currentCheckpoint = checkpoint;
        
        // TODO: Register game state
        Camera.RegisterState();
    }

    public void SetGamestate(Gamestate state)
    {
        _gamestate = state;

        switch (_gamestate)
        {
            case Gamestate.Playing:
            case Gamestate.Paused:
                throw new NotImplementedException();
            
           case Gamestate.GameOver:
               // TODO: Better game over handling
               Character.transform.DOMove(_currentCheckpoint.position, 1f);
               Camera.ResetState();
               break;
           
           default:
                break;
        }
    }

    public Gamestate GetGamestate()
    {
        return _gamestate;
    }
}
