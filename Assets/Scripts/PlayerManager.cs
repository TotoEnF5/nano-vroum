using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<GameObject> players;
    public Color[] colors;
    void Awake()
    {
        players = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count == 2)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<SpriteRenderer>().color = colors[i];
            }
            Destroy(gameObject);
        }
    }
}
