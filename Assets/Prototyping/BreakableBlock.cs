using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public int health = 1;

    public List<Sprite> sprites;
    public SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.sprite = sprites[health - 1];
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            health--;
            if(health == 0)
            {
                CameraShake.StartShake(0.3f, 2f);
                Destroy(gameObject);
            }
            else
            {
                CameraShake.StartShake(0.2f, 1f);
                spriteRenderer.sprite = sprites[health - 1];
            }
        }
    }
}
