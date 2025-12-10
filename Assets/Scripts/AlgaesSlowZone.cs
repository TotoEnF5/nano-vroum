using UnityEngine;

public class AlgaesSlowZone : MonoBehaviour
{
    public float PlayerMassIncrease = 40;
    public float PlayerLinearDampingIncrease = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.mass = PlayerMassIncrease;
            rb.linearDamping = PlayerLinearDampingIncrease;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().mass = 10;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().mass = 1;
            collision.GetComponent<Rigidbody2D>().linearDamping = 0;
        }
    }
}
