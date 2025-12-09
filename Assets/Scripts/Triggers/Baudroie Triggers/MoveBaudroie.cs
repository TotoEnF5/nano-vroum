using DG.Tweening;
using UnityEngine;

public class MoveBaudroie : MonoBehaviour
{
    private struct BaudroieState
    {
        public Vector3 InitPos;
        public Vector3 GoalPos;
        public float Time;
        public float Elapsed;
        public bool WasTweening;
    }

    public enum BaudroieDestination
    {
        BaudroieTrigger,
        Custom,
    }

    private BaudroieState _lastState, _registeredState;
    private Tween _movementTween;

    private void Awake()
    {
        _lastState.InitPos = transform.position;
        _lastState.GoalPos = transform.position;
        _lastState.Time = 0f;
        _lastState.Elapsed = 0f;
        _lastState.WasTweening = false;
    }
    
    public void StartMovement(Transform goal, float time, Ease ease)
    {
        StartMovement(goal.position, time, ease);
    }

    public void StartMovement(Vector3 goal, float time, Ease ease)
    {
        Debug.LogError("movement started");
        goal.z = transform.position.z;

        _lastState.InitPos = transform.position;
        _lastState.GoalPos = goal;
        _lastState.Time = time;
        _lastState.Elapsed = 0f;
        _lastState.WasTweening = true;
        
        _movementTween = transform.DOMove(goal, time).SetEase(ease);
    }
    
    public void RegisterState()
    {
        _registeredState.WasTweening = _lastState.WasTweening;
        _registeredState.InitPos = _lastState.InitPos;
        _registeredState.GoalPos = _lastState.GoalPos;
        _registeredState.Time = _lastState.Time;
        Debug.LogError(_registeredState.InitPos);
        Debug.LogError(_registeredState.WasTweening);

        if (_movementTween != null)
        {
            _registeredState.Elapsed = _movementTween.Elapsed();
        }
    }
    
    public void ResetState()
    {
        _movementTween?.Pause();
        _movementTween?.Kill();
        
        Debug.LogError(_registeredState.InitPos);
        Debug.LogError(_registeredState.WasTweening);

        transform.position = _registeredState.InitPos;

        if (_registeredState.WasTweening)
        {
            Debug.LogError("other movement started");
            _movementTween = transform.DOMove(_registeredState.GoalPos, _registeredState.Time).SetEase(Ease.Linear);
            _movementTween.Goto(_registeredState.Elapsed);
        }
    }
}
