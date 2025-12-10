using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StartGameScript : MonoBehaviour
{
    public GameObject CameraTrigger;
    public GameObject BaudroieTrigger;

    public GameObject Light;
    public GameObject CameraTrigger2;

    public GameObject lightPreset1;
    public GameObject lightPreset2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightPreset2.gameObject.SetActive(false);
        lightPreset1.gameObject.SetActive(true);
        CameraTrigger2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Camera.main.GetComponent<CameraShake>().duration = 3f;
            Camera.main.GetComponent<CameraShake>().intensity = 1f;
            collision.gameObject.transform.DOPunchScale(Vector3.one * 1.5f, 1f, 1).SetEase(Ease.OutQuint).onUpdate += () => {
                collision.gameObject.transform.localScale = Vector3.one;
                StartCoroutine(corStartCameras());
            }; ;
            Light.SetActive(false);
        }

    }
    public IEnumerator corStartCameras()
    {
        CameraTrigger.gameObject.SetActive(true);
        BaudroieTrigger.gameObject.SetActive(true);


        Light2D globalLight1 = lightPreset1.GetComponent<Light2D>();
        DOTween.To(() => globalLight1.intensity, x => globalLight1.intensity = x, 0, 1f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1);
        lightPreset1.gameObject.SetActive(false);
        Light2D globalLight = lightPreset2.GetComponent<Light2D>();
        globalLight.intensity = 0;
        yield return new WaitForSeconds(1);
        lightPreset2.SetActive(true);
        yield return new WaitForSeconds(.2f);
        DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 1, 1f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(4);

        CameraTrigger2.gameObject.SetActive(true);
    }

}
