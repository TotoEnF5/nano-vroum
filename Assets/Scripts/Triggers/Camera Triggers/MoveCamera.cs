using UnityEngine;
using DG.Tweening;

public class MoveCamera : MonoBehaviour
{
    private struct CameraState
    {
        public Vector3 InitPos;
        public float InitSize;
        public Vector3 Goal;
        public float Size;
        public float Time;
    }
    
    /**
     * Where the camera should end up relative to the trigger.
     */
    public enum CameraDestination
    {
        Top,            // The camera goes at the top of the trigger
        Bottom,         // The camera goes at the bottom of the trigger
        Left,           // The camera goes at the left of the trigger
        Right,          // The camera goes at the right of the trigger
        CameraTrigger,  // The camera scrolls to another camera trigger
        Custom,         // The camera goes to a user-defined location
    }

    private Camera _camera;
    private Tween _movementTween, _sizeTween;
    private CameraState _lastState, _registeredState;
    private float _movementElapsed, _sizeElapsed;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public void StartMovement(Transform goal, CameraDestination cameraDestination, float newSize, float time, Ease ease, bool matchX, bool matchY)
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

        if (!matchX)
        {
            destination.x = transform.position.x;
        }
        
        if (!matchY)
        {
            destination.y = transform.position.y;
        }
        
        StartMovement(destination, newSize, time, ease);
    }

    public void StartMovement(Transform goal, float newSize, float time, Ease ease)
    {
        StartMovement(goal.position, newSize, time, ease);
    }

    public void StartMovement(Vector3 goal, float newSize, float time, Ease ease)
    {
        goal.z = transform.position.z;

        _lastState.InitPos = transform.position;
        _lastState.InitSize = _camera.orthographicSize;
        _lastState.Goal = goal;
        _lastState.Size = newSize;
        _lastState.Time = time;
        
        // Allez hop tweenez moi ça
        _movementTween = transform.DOMove(goal, time).SetEase(ease);
        _sizeTween = DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, newSize, time);
    }

    public void RegisterState()
    {
        _registeredState = _lastState;

        if (_movementTween != null)
        {
            _movementElapsed = _movementTween.Elapsed();
        }

        if (_sizeTween != null)
        {
            _sizeElapsed = _sizeTween.Elapsed();
        }
    }

    public void ResetState()
    {
        _movementTween?.Kill();
        _sizeTween?.Kill();

        transform.position = _registeredState.InitPos;
        _camera.orthographicSize = _registeredState.InitSize;

        _movementTween = transform.DOMove(_registeredState.Goal, _registeredState.Time).SetEase(Ease.Linear);
        _sizeTween = DOTween.To(x => _camera.orthographicSize = x, _camera.orthographicSize, _registeredState.Size, _registeredState.Time);
        
        _movementTween.Goto(_movementElapsed, true);
        _sizeTween.Goto(_sizeElapsed, true);
    }
}
