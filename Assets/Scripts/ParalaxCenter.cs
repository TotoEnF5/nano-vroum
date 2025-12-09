using DG.Tweening;
using UnityEngine;

public class ParalaxCenter : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, cam.gameObject.transform.position.y, transform.position.z);
    }
}
