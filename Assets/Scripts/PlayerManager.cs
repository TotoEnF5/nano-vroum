using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [HideInInspector]
    public List<GameObject> players;
    public Color[] colors;
    public Color inactive;
    private bool isInitDone = false;
    private int activePlayerIndex = 0;

    public List<Gamepad> gamepads = new List<Gamepad>();

    public Transform LineTransform;
    void Awake()
    {
        players = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count == 2 && !isInitDone)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (i > 0)
                {
                    players[i].GetComponentInChildren<SpriteRenderer>().color = inactive;
                }
                else
                {
                    players[i].GetComponentInChildren<SpriteRenderer>().color = colors[i];
                }

                players[i].GetComponent<Cursors>().inactiveColor = inactive;
                players[i].GetComponent<Cursors>().activeColor = colors[i];
            }
            players[1].gameObject.transform.position += Vector3.up * 5;
            isInitDone = true;
            SetPlayerTurn(activePlayerIndex);
            PlaceCursors();
        }


    }
    public void PlaceCursors()
    {
        players[0].transform.DOMove(LineTransform.position - LineTransform.transform.up * 3,.5f);
        players[1].transform.DOMove(LineTransform.position + LineTransform.transform.up * 3,.5f);
    }

    private void SetPlayerTurn(int index)
    {
        for (int i = 0; i < players.Count; i++)
        {
            Cursors playerCursor = players[i].GetComponent<Cursors>();
            if (playerCursor != null)
            {
                bool isMyTurn = (i == index);
                playerCursor.SetCanAct(isMyTurn);
            }
        }
    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Gamepad device = playerInput.GetDevice<Gamepad>();
        gamepads.Add(device);
    }
    public void EndTurn()
    {
        // Change l'index du joueur actif (0 -> 1, 1 -> 0)
        activePlayerIndex = 1 - activePlayerIndex;
        for (int i = 0; i < players.Count; i++)
        {
            if (i == activePlayerIndex)
            {
                RumbleManager.Instance.Rumble(gamepads[i], 0.053f, 0.126f, 0.1f);
                players[i].GetComponentInChildren<SpriteRenderer>().color = colors[i];
            }

            else
            {
                RumbleManager.Instance.Rumble(gamepads[i], 0.123f, 0.356f, 0.3f);
                players[i].GetComponentInChildren<SpriteRenderer>().color = inactive;
            }

        }

        // Applique l'autorisation au nouveau joueur
        SetPlayerTurn(activePlayerIndex);

        Debug.Log($"Fin du tour. C'est au joueur {activePlayerIndex + 1} de jouer.");

    }
}
