using UnityEngine;
using DG.Tweening;

public class MoveCamera : MonoBehaviour
{
    /**
     * Where the camera should end up relative to the trigger.
     */
    public enum CameraDestination
    {
        Top,    // The camera goes at the top of the trigger
        Bottom, // The camera goes at the bottom of the trigger
        Left,   // The camera goes at the left of the trigger
        Right,  // The camera goes at the right of the trigger
    }
    
    private float _camWidth, _camHeight;

    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        _camHeight = 2 * camera.orthographicSize;
        _camWidth = _camHeight * camera.aspect;
        Debug.Log(_camWidth);
        Debug.Log(_camHeight);
    }

    public void StartMovement(Transform goal, float time, CameraDestination cameraDestination)
    {
        Vector3 destination = goal.position;
        
        switch (cameraDestination)
        {
            case CameraDestination.Top:
                destination.y += _camHeight / 2 + goal.localScale.y / 2;
                break;
            case CameraDestination.Bottom:
                destination.y -= _camHeight / 2 + goal.localScale.y / 2;
                break;
            case CameraDestination.Left:
                destination.x -= _camWidth / 2 + goal.localScale.x / 2;
                break;
            case CameraDestination.Right:
                destination.x += _camWidth / 2 + goal.localScale.x / 2;
                break;
            default:
                break;
        }

        destination.z = transform.position.z;
        transform.DOMove(destination, time);
    }
}
