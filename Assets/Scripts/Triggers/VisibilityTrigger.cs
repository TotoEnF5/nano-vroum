using System;
using UnityEngine;

public class VisibilityTrigger : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public Vector3 position;
    public Quaternion rotation;
    public bool oneShot = true;

    private bool _spawned = false;

    private void OnBecameVisible()
    {
        if (_spawned)
        {
            return;
        }
        
        Instantiate(prefabToInstantiate, position, rotation);
        _spawned = true && oneShot;
    }
}
