using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image progressBar;
    
    public void LoadScene(string name)
    {
        progressBar.fillAmount = 0f;
        StartCoroutine(LoadSceneAsync(name));
    }

    private IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            DOTween.To((x) => { progressBar.fillAmount = x; }, progressBar.fillAmount, progress, 0.1f);
            yield return null;
        }
    }
}
