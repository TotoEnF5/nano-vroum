using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class DestroyWhenCollidingWithTag : MonoBehaviour
{
    public string Tag = "Destroy Trigger";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag))
        {
            Destroy(gameObject);
        }
    }
}
