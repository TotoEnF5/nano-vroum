using System;
using DG.Tweening;
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
    public static Transform Character;
    public static Transform CurrentCheckpoint;
    
    private static Gamestate _gamestate = Gamestate.Playing;
    
    private InputAction _pauseAction;

    private void Awake()
    {
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

    public static void SetGamestate(Gamestate state)
    {
        _gamestate = state;

        switch (_gamestate)
        {
            case Gamestate.Playing:
            case Gamestate.Paused:
                throw new NotImplementedException();
            
           case Gamestate.GameOver:
               // TODO: Better game over handling
               Character.transform.DOMove(CurrentCheckpoint.position, 1f);
               break;
           
           default:
                break;
        }
    }

    public static Gamestate GetGamestate()
    {
        return _gamestate;
    }
}
