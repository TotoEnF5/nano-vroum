using System;
using DG.Tweening;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Gamestate
{
    Playing,
    Paused,
    GameOver,
    Win,
}

public class GamestateManager : MonoBehaviour
{
    public static GamestateManager Instance { get; private set; }
    
    public Transform character;
    public MoveCamera cameraScript;
    public MoveBaudroie baudroie;
    public Image image;
    
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

    public void IncreaseSpeed()
    {
        DOTween.timeScale *= 1.2f;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        _currentCheckpoint = checkpoint;
        
        cameraScript.RegisterState();
        baudroie.RegisterState();
    }

    public void SetGamestate(Gamestate state)
    {
        _gamestate = state;

        switch (_gamestate)
        {
            case Gamestate.Playing:
            case Gamestate.Paused:
            case Gamestate.Win:
                throw new NotImplementedException();
            
           case Gamestate.GameOver:
               DoGameOverAnimation();
               break;
           
           default:
                break;
        }
    }

    public Gamestate GetGamestate()
    {
        return _gamestate;
    }

    private void DoGameOverAnimation()
    {
        DOTween.timeScale = 1f;
        
        DOTween.To((x) =>
        {
            Color color = image.color;
            color.a = x;
            image.color = color;
        }, image.color.a, 1f, 2f)
        .OnComplete(() => {
            character.position = _currentCheckpoint.position;
            
            cameraScript.ResetState();
            baudroie.ResetState();

            GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");
            foreach (GameObject trigger in triggers)
            {
                Trigger t = trigger.GetComponent<Trigger>();
                if (t != null)
                {
                    t.ResetState();
                }

                BaudroieTrigger bt = trigger.GetComponent<BaudroieTrigger>();
                if (bt != null)
                {
                    bt.ResetState();
                }
                
                VisibilityTrigger vt = trigger.GetComponent<VisibilityTrigger>();
                if (vt != null)
                {
                    vt.ResetState();
                }
            }
            
            Color color = image.color;
            color.a = 0f;
            image.color = color;
        });
    }
}
