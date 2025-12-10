using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    Button button;
    [SerializeField] Sprite baseSprite;
    [SerializeField] Sprite selectedSprite;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void OnDeselect(BaseEventData eventData)
    {
        button.image.sprite = baseSprite;
    }

    public void OnSelect(BaseEventData eventData)
    {
        button.image.sprite = selectedSprite;
    }
}
