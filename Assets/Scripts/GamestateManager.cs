using System;
using DG.Tweening;
using UnityEngine;

public enum Gamestate
{
    Playing,
    Paused,
    GameOver,
}

public class GamestateManager : MonoBehaviour
{
    public static Transform Character;
    public static Transform CurrentCheckpoint;
    
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
               // TODO: Better game over handling
               Character.transform.DOMove(CurrentCheckpoint.position, 1f);
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
