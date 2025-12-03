using UnityEngine;

public class Cursors : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        Vector3 pos2 = Camera.main.ScreenToWorldPoint(pos);
        transform.position = new Vector3(pos2.x, pos2.y, -1);
    }
}
