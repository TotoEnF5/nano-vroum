using UnityEngine;

public class IncreaseBoundingBox : MonoBehaviour
{
    SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.bounds = new Bounds(Vector3.zero, Vector3.one * 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
