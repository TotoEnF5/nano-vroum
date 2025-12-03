using Unity.VisualScripting;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public enum SideDestination
    {
        Top,
        Bottom,
        Left,
        Right,
    }
    
    [SerializeField] private MoveCamera cameraScript;
    [SerializeField] private bool showInGame = false;
    [SerializeField] private float time = 0.5f;
    [SerializeField] private SideDestination sideDestination;

    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraScript.StartMovement(transform.position, time, sideDestination, transform.localScale);
        }
    }
}
