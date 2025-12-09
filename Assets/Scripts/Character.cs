using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject friendPrefab;
    private Rigidbody2D _rigidBody;
    private Camera mainCamera;
    private ParticleSystem CollidePs;

    private void Awake()
    {
        CollidePs = GetComponentInChildren<ParticleSystem>();
        _rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ClampPositionToScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidBody.linearVelocity = Vector3.zero;
        CollidePs.Play();

        if(collision.gameObject.CompareTag("killer"))
        {
            GamestateManager.Instance.SetGamestate(Gamestate.GameOver);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LostFriend"))
        {
            AddFriend();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Baudroie"))
        {
            GamestateManager.Instance.SetGamestate(Gamestate.GameOver);
        }
    }

    private void ClampPositionToScreen()
    {
        if (mainCamera == null)
        {
            return;
        }

        Vector3 minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - mainCamera.transform.position.z));
        Vector3 maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - mainCamera.transform.position.z));

        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(
            currentPosition.x,
            minScreenBounds.x,
            maxScreenBounds.x
        );

        currentPosition.y = Mathf.Clamp(
            currentPosition.y,
            minScreenBounds.y,
            maxScreenBounds.y
        );

        transform.position = currentPosition;
    }

    private void AddFriend()
    {
        GameObject friend = Instantiate(friendPrefab, transform);
        
        // Set position
        Vector2 random = Random.insideUnitCircle * 2;
        Vector3 randomVec3 = new Vector3(random.x, random.y, 0);
        randomVec3 += transform.position;
        friend.transform.position = randomVec3;
        
        // TODO: When getting a new friend, increase the moving speed
        GamestateManager.Instance.IncreaseSpeed();
    }
}
