using DG.Tweening;
using System.Collections;
using UnityEngine;

public class StartGameScript : MonoBehaviour
{
    public GameObject CameraTrigger;
    public GameObject BaudroieTrigger;

    public GameObject Light;
    public GameObject CameraTrigger2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            
            collision.gameObject.transform.DOPunchScale(Vector3.one * 1.5f, 1f, 1).SetEase(Ease.OutQuint).onUpdate += () => {
                StartCoroutine(corStartCameras());
            }; ;

            Light.SetActive(false);
        }

    }
    public IEnumerator corStartCameras()
    {
        CameraTrigger.gameObject.SetActive(true);
        BaudroieTrigger.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        CameraTrigger2.gameObject.SetActive(true);
    }

}
