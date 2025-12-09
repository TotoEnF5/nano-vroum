using DG.Tweening;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public MoveCamera cameraScript;
    public bool doSomething = true;
    public bool showInGame = false;
    public Ease ease = Ease.Linear;
    public float time = 2;
    public bool oneShot = true;
    public bool matchX = true;
    public bool matchY = true;
    public MoveCamera.CameraDestination cameraDestination;
    public Transform scrollingDestination;
    public Vector3 customPosition;
    [Min(0)] public float newCameraSize = 5;

    private bool _triggered = false;
    
    private void Awake()
    {
        Show(showInGame);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered || !doSomething || !other.CompareTag("Player"))
        {
            return;
        }
        
        if (cameraDestination == MoveCamera.CameraDestination.Custom)
        {
            cameraScript.StartMovement(customPosition, newCameraSize, time, ease);
        }
        else if (cameraDestination == MoveCamera.CameraDestination.CameraTrigger)
        {
            cameraScript.StartMovement(scrollingDestination, newCameraSize, time, ease);
        }
        else
        {
            cameraScript.StartMovement(transform, cameraDestination, newCameraSize, time, ease, matchX, matchY);
        }

        _triggered = oneShot;
    }

    public void Show(bool show)
    {
        GetComponent<Renderer>().enabled = show;
    }

    public void ResetState()
    {
        _triggered = false;
    }
}
