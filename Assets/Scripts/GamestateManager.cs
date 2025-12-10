using System;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
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
    public StartGameScript startGameScript;
    public Chrono chrono;
    public TMP_Text bravo;
    public PlayerManager PlayerManager;
    
    private Transform _currentCheckpoint;
    private Vector3 _initPlayerPos;
    private Gamestate _gamestate = Gamestate.Playing;
    private bool _doingGameOverAnimation = false;
    
    private InputAction _pauseAction;

    public float GlobalTime = 1;
    public float GlobalTimeIncrement = 0.2f;

    public Color Color;
    private bool mustRestartGame = false;
    private void Awake()
    {
        Shader.SetGlobalColor("_GlobalColor", Color);
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _initPlayerPos = character.position;
        _pauseAction = InputSystem.actions.FindAction("Pause");
        
        bravo.gameObject.SetActive(false);
    }
    
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale = 2;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale = 0.5f;

        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Time.timeScale = 1;
        }
#endif
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
        DOTween.timeScale += (GlobalTimeIncrement*2);
        GlobalTime += (GlobalTimeIncrement*2);
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        _currentCheckpoint = checkpoint;
        if(checkpoint.tag == "StartGameCheckpoint")
        {
            mustRestartGame = true;
        }
        else
        {
            mustRestartGame = false;
        }

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
                throw new NotImplementedException();
            
            case Gamestate.Win:
                DoWinAnimation();
                break;
            
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
        if (_doingGameOverAnimation)
        {
            return;
        }
        startGameScript.BaudroieTrigger.gameObject.SetActive(false);
        
        _doingGameOverAnimation = true;
        DOTween.timeScale = 1f;
        GlobalTime = 1;

        DOTween.To((x) =>
        {
            Color color = image.color;
            color.a = x;
            image.color = color;
        }, image.color.a, 1f, 1f)
        .OnComplete(() =>
        {
            if (_currentCheckpoint != null)
            {
                character.position = _currentCheckpoint.position;
            }
            else
            {
                character.position = _initPlayerPos;
            }

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

                ScrollingSection s = trigger.GetComponent<ScrollingSection>();
                if (s != null)
                {
                    s.ResetState();
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

            PlayerManager.PlaceCursors();   
            _doingGameOverAnimation = false;
            if (mustRestartGame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            DOTween.To((x) =>
            {
                Color color = image.color;
                color.a = x;
                image.color = color;
            }, image.color.a, 0f, 0.5f);
        });
    }

    public void DoWinAnimation()
    {
        DOTween.To((x) =>
        {
            Color color = image.color;
            color.a = x;
            image.color = color;
        }, image.color.a, 1f, 2f)
        .OnComplete(() =>
        {
            String baseText = bravo.text;
            float time = chrono.time;
            bravo.text += time + " seconds.";
            bravo.gameObject.SetActive(true);

            DOTween.To((x) => { }, 0, 1, 10f).
                OnComplete(() =>
                {
                    bravo.text = baseText;
                    SceneManager.LoadScene("MenuMain");
                });
        });
    }
}
