using Unity.VisualScripting;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public MoveCamera cameraScript;
    public bool doSomething = true;
    public bool showInGame = false;
    public float time = 2;
    public bool matchX = true;
    public bool matchY = true;
    public MoveCamera.CameraDestination cameraDestination;
    public Transform scrollingDestination;
    public Vector3 customPosition;
    [Min(0)] public float newCameraSize = 5;

    private void Awake()
    {
        Show(showInGame);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!doSomething)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (cameraDestination == MoveCamera.CameraDestination.Custom)
            {
                cameraScript.StartMovement(customPosition, newCameraSize, time);
            }
            else if (cameraDestination == MoveCamera.CameraDestination.CameraTrigger)
            {
                cameraScript.StartMovement(scrollingDestination, newCameraSize, time);
            }
            else
            {
                cameraScript.StartMovement(transform, cameraDestination, newCameraSize, time, matchX, matchY);
            }
        }
    }

    public void Show(bool show)
    {
        GetComponent<Renderer>().enabled = show;
    }
}
