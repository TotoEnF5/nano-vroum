using UnityEngine;
using DG.Tweening;

public class MoveCamera : MonoBehaviour
{
    private float _camWidth, _camHeight;

    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        _camHeight = 2 * camera.orthographicSize;
        _camWidth = _camHeight * camera.aspect;
        Debug.Log(_camWidth);
        Debug.Log(_camHeight);
    }

    public void StartMovement(Transform goal, float time, Trigger.CameraDestination cameraDestination)
    {
        Vector3 destination = goal.position;
        
        switch (cameraDestination)
        {
            case Trigger.CameraDestination.Top:
                destination.y += _camHeight / 2 + goal.localScale.y / 2;
                break;
            case Trigger.CameraDestination.Bottom:
                destination.y -= _camHeight / 2 + goal.localScale.y / 2;
                break;
            case Trigger.CameraDestination.Left:
                destination.x -= _camWidth / 2 + goal.localScale.x / 2;
                break;
            case Trigger.CameraDestination.Right:
                destination.x += _camWidth / 2 + goal.localScale.x / 2;
                break;
            default:
                break;
        }

        destination.z = transform.position.z;
        transform.DOMove(destination, time);
    }
}
