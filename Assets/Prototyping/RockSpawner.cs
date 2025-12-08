using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public List<GameObject> RockPrefabs;

    public float SpawnOffset = 0;
    public float RockSpawnTimer = 3;
    public float RockScaleMin = 1;
    public float RockScaleMax = 3;
    private float t;

    public bool ApplyForce = false;
    public Vector2 ForceApplied = Vector2.zero;

    public Camera MainCamera;

    /// <summary>
    /// Activate only once it appears on camera
    /// </summary>
    public bool TriggerWhenOnCamera;
    private bool m_triggeredOnce = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t += Time.deltaTime;
        if (!m_triggeredOnce && TriggerWhenOnCamera)
        {
            Vector3 viewPos = MainCamera.WorldToViewportPoint(transform.position);
            if(viewPos.x >= 0 && viewPos.x <=1 && viewPos.y >=0 && viewPos.y <= 1)
            {
                SpawnRock();
            }
        }
        else if(!TriggerWhenOnCamera)
        {
            SpawnerLogic();
        }
    }
    public void SpawnerLogic()
    {
        if (t >= RockSpawnTimer + SpawnOffset)
        {
            t = SpawnOffset;
            SpawnRock();
        }
    }
    public void SpawnRock()
    {
        m_triggeredOnce = true;
        GameObject Rock = GameObject.Instantiate(RockPrefabs[Random.Range(0,RockPrefabs.Count)], transform);
        Rock.transform.localScale = Vector3.one * UnityEngine.Random.Range(Mathf.Min(RockScaleMin, RockScaleMax), Mathf.Max(RockScaleMax, RockScaleMin));
        if (ApplyForce)
        {
            Rigidbody2D rb = Rock.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = Rock.AddComponent<Rigidbody2D>();
            }
            rb.AddForceAtPosition(ForceApplied, Rock.transform.position, ForceMode2D.Impulse);
        }
    }
}
