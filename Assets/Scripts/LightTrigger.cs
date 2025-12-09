using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightTrigger : MonoBehaviour
{
    public bool showInGame = false;
    
    private bool _triggered = false;
    public Color newLightColor;
    public float newLightValue = 1;
    public float triggerDuration = 1f;
    public Light2D GlobalLight;
    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered || !other.CompareTag("Player"))
        {
            return;
        }

        DOTween.To(() => GlobalLight.intensity, x => GlobalLight.intensity = x, newLightValue, triggerDuration).SetEase(Ease.OutQuad);
        DOTween.To(() => GlobalLight.color, x => GlobalLight.color = x, newLightColor, triggerDuration).SetEase(Ease.OutQuad);


        _triggered = true;
    }
}
