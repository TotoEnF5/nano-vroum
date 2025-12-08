using DG.Tweening;
using UnityEngine;

public class MoveBaudroie : MonoBehaviour
{
    private struct BaudroieState
    {
        public Vector3 InitPos;
        public Vector3 Goal;
        public float Time;
    }

    public enum BaudroieDestination
    {
        BaudroieTrigger,
        Custom,
    }

    private BaudroieState _lastState, _registeredState;
    private Tween _movementTween;
    private float _movementElapsed;
    
    public void StartMovement(Transform goal, float time)
    {
        StartMovement(goal.position, time);
    }

    public void StartMovement(Vector3 goal, float time)
    {
        goal.z = transform.position.z;

        _lastState.InitPos = transform.position;
        _lastState.Goal = goal;
        _lastState.Time = time;
        
        Debug.Log(goal);

        _movementTween = transform.DOMove(goal, time).SetEase(Ease.Linear);
    }
    
    public void RegisterState()
    {
        _registeredState = _lastState;

        if (_movementTween != null)
        {
            _movementElapsed = _movementTween.Elapsed();
        }
    }
    
    public void ResetState()
    {
        _movementTween?.Kill();

        transform.position = _registeredState.InitPos;

        _movementTween = transform.DOMove(_registeredState.Goal, _registeredState.Time).SetEase(Ease.Linear);
        _movementTween.Goto(_movementElapsed, true);
    }
}
