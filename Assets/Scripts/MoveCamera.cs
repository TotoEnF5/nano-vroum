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

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public void StartMovement(Transform goal, float time, CameraDestination cameraDestination, float newSize = 0)
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
        destination.z = transform.position.z;
        
        // Allez hop tweenez moi ça
        transform.DOMove(destination, time);
        DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, newSize, time);
    }
}
