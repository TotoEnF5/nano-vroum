using System;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Camera _camera;
    
    private Vector3 _initialPosition;
    private Vector3 _goalPosition;
    private float _lerpTime = 0.0f;
    private float _t = 0.0f;
    private bool _doLerp = false;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!_doLerp)
        {
            return;
        }
        
        float z = transform.position.z;
            
        _t += Time.deltaTime / _lerpTime;
        Vector3 newPos = Vector3.Lerp(_initialPosition, _goalPosition, _t);
        newPos.z = transform.position.z;
        transform.position = newPos;

        _doLerp = _t < 1.0f;
    }

    public void StartMovement(Vector3 goal, float time)
    {
        _initialPosition = transform.position;
        _goalPosition = new Vector3(goal.x, goal.y - _camera.orthographicSize, goal.z);
        _lerpTime = time;
        _doLerp = true;
    }
}
