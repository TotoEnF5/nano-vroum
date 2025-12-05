using DG.Tweening;
using UnityEngine;

public class BaudroieDestinationTrigger : MonoBehaviour
{
    public Ease ease = Ease.Linear;
    public float time = 1f;

    [HideInInspector] public bool showInGame;
    
    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }
}
