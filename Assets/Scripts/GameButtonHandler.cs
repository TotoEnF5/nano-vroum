using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameButtonHandler : MonoBehaviour
{
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
}
