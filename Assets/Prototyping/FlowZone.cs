using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FlowZone : MonoBehaviour
{
    private Rigidbody2D player;
    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float maxSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null && player.linearVelocity.magnitude < maxSpeed)
        {
            player.AddForce(Vector2.up * acceleration, ForceMode2D.Force);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        player.linearVelocity = Vector2.zero;
        player = null;
    }
}
