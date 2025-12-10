using UnityEngine;
using DG.Tweening;

public class MoveCamera : MonoBehaviour
{
    private struct CameraState
    {
        public Vector3 InitPos;
        public float InitSize;
        public Vector3 GoalPos;
        public float GoalSize;
        public float Time;
        public float Elapsed;
        public bool WasTweening;
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

    public Camera camera;
    
    private Tween _movementTween, _sizeTween;
    private CameraState _lastState, _registeredState;

    private void Awake()
    {
        _lastState.InitPos = transform.position;
        _lastState.InitSize = camera.orthographicSize;
        _lastState.Time = 0f;
        _lastState.Elapsed = 0f;
        _lastState.WasTweening = false;
        
        _registeredState.InitPos = transform.position;
        _registeredState.InitSize = camera.orthographicSize;
        _registeredState.Time = 0f;
        _registeredState.Elapsed = 0f;
        _registeredState.WasTweening = false;
    }

    public void StartMovement(Transform goal, CameraDestination cameraDestination, float newSize, float time, Ease ease, bool matchX, bool matchY)
    {
        // New camera size
        float camHeight = 2 * newSize;
        float camWidth = camHeight * camera.aspect;
        
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
        _lastState.InitSize = camera.orthographicSize;
        _lastState.GoalPos = goal;
        _lastState.GoalSize = newSize;
        _lastState.Time = time;
        _lastState.Elapsed = 0f;
        _lastState.WasTweening = true;
        
        // Allez hop tweenez moi ça
        _movementTween = transform.DOMove(goal, time).SetEase(ease);
        _sizeTween = DOTween.To(x => camera.orthographicSize = x, camera.orthographicSize, newSize, time);
    }

    public void RegisterState()
    {
        _registeredState.WasTweening = _lastState.WasTweening;
        _registeredState.InitPos = _lastState.InitPos;
        _registeredState.InitSize = _lastState.InitSize;
        _registeredState.GoalPos = _lastState.GoalPos;
        _registeredState.GoalSize = _lastState.GoalSize;
        _registeredState.Time = _lastState.Time;

        if (_movementTween != null)
        {
            _registeredState.Elapsed = _movementTween.Elapsed();
        }
    }

    public void ResetState()
    {
        _movementTween?.Pause();
        _movementTween?.Kill();
        _sizeTween?.Pause();
        _sizeTween?.Kill();

        transform.position = _registeredState.InitPos;
        camera.orthographicSize = _registeredState.InitSize;

        if (_registeredState.WasTweening)
        {
            _movementTween = transform.DOMove(_registeredState.GoalPos, _registeredState.Time).SetEase(Ease.Linear);
            _sizeTween = DOTween.To(x => camera.orthographicSize = x, camera.orthographicSize, _registeredState.GoalSize, _registeredState.Time);
            
            _movementTween.Goto(_registeredState.Elapsed, true);
            _sizeTween.Goto(_registeredState.Elapsed, true);
        }
    }
}
