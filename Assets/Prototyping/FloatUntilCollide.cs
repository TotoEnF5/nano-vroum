using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class FloatUntilCollide : MonoBehaviour
{

    public float minOffset = 1;
    public float maxOffset = 3;
    public float offset;

    public float amplitude = .5f;
    public float speed = .5f;
    private bool mustFloat = true;
    private float t;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = Random.Range(minOffset, maxOffset);    
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (mustFloat && t >= offset)
        {
            transform.localPosition= new Vector3(transform.localPosition.x, transform.localPosition.y + amplitude *Mathf.Sin(Time.realtimeSinceStartup * speed), 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        mustFloat = false;
    }
}
