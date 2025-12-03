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
        Custom, // The camera goes to a user-defined location
    }

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public void StartMovement(Transform goal, CameraDestination cameraDestination, float newSize, float time)
    {
        // New camera size
        float camHeight = 2 * newSize;
        float camWidth = camHeight * _camera.aspect;
        
        // New camera coords
        Vector3 destination = goal.position;
        switch (cameraDestination)
        {
            case CameraDestination.Top:
                destination.y += camHeight / 2 + goal.localScale.y / 2;
                break;
            case CameraDestination.Bottom:
                destination.y -= camHeight / 2 + goal.localScale.y / 2;
                break;
            case CameraDestination.Left:
                destination.x -= camWidth / 2 + goal.localScale.x / 2;
                break;
            case CameraDestination.Right:
                destination.x += camWidth / 2 + goal.localScale.x / 2;
                break;
            default:
                break;
        }
        
        StartMovement(destination, newSize, time);
    }

    public void StartMovement(Vector3 goal, float newSize, float time)
    {
        goal.z = transform.position.z;
        
        // Allez hop tweenez moi ça
        transform.DOMove(goal, time);
        DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, newSize, time);
    }
}
