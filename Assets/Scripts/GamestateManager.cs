using System;
using UnityEngine;

public enum Gamestate
{
    Playing,
    Paused,
    GameOver,
}

public class GamestateManager : MonoBehaviour
{
    private static Gamestate _gamestate = Gamestate.Playing;

    public static void SetGamestate(Gamestate state)
    {
        _gamestate = state;

        switch (_gamestate)
        {
            case Gamestate.Playing:
            case Gamestate.Paused:
                throw new NotImplementedException();
            
           case Gamestate.GameOver:
               Debug.Log("oh no");
               break;
           
           default:
                break;
        }
    }

    public static Gamestate GetGamestate()
    {
        return _gamestate;
    }
}
