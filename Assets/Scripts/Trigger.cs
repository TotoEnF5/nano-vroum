using Unity.VisualScripting;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private MoveCamera cameraScript;
    [SerializeField] private bool showInGame = false;
    [SerializeField] private float time = 2;
    [SerializeField] private bool matchX = true;
    [SerializeField] private bool matchY = true;
    [SerializeField] private MoveCamera.CameraDestination cameraDestination;
    [SerializeField] private Transform scrollingDestination;
    [SerializeField] private Vector3 customPosition;
    [SerializeField, Min(0)] private float newCameraSize = 5;

    private void Awake()
    {
        GetComponent<Renderer>().enabled = showInGame;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
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

    public void SetCameraScript(MoveCamera script)
    {
        cameraScript = script;
    }

    public void SetTime(float t)
    {
        time = t;
    }
}
