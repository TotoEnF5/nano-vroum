using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject friendPrefab;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        _rigidBody.MovePosition(pos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LostFriend"))
        {
            AddFriend();
            Destroy(other.gameObject);
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
