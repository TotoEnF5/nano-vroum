using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject friendPrefab;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        GamestateManager.Character = this.transform;
        _rigidBody = GetComponent<Rigidbody2D>();
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
            GamestateManager.SetGamestate(Gamestate.GameOver);
        }
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
    }
}
