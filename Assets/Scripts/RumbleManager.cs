using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Fait vibrer la manette.
    /// </summary>
    /// <param name="player">Joueur concerné par la vibration</param>
    /// <param name="lowFrequency">Fréquence basse de la vibration</param>
    /// <param name="highFrequency">Fréquence hausse de la vibration</param>
    /// <param name="duration">Durée de la vibration</param>
    public void Rumble(Gamepad gamepad, float lowFrequency, float highFrequency, float duration)
    {
        StartCoroutine(_RumbleCoroutine(gamepad, lowFrequency, highFrequency, duration));
    }

    /// <summary>
    /// Fait vibrer la dernière manette a avoir input.
    /// </summary>
    /// <param name="lowFrequency">Fréquence basse de la vibration</param>
    /// <param name="highFrequency">Fréquence hausse de la vibration</param>
    /// <param name="duration">Durée de la vibration</param>
    public void RumbleCurrent(float lowFrequency, float highFrequency, float duration)
    {
        StartCoroutine(_RumbleCurrentCoroutine(lowFrequency, highFrequency, duration));
    }

    private IEnumerator _RumbleCoroutine(Gamepad gamepad, float lowFrequency, float highFrequency, float duration)
    {
        gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
        yield return new WaitForSeconds(duration);
        gamepad.SetMotorSpeeds(0, 0);
    }
        private IEnumerator _RumbleCurrentCoroutine(float lowFrequency, float highFrequency, float duration)
    {
        Gamepad current = Gamepad.current;
        current.SetMotorSpeeds(lowFrequency, highFrequency);
        yield return new WaitForSeconds(duration);
        current.SetMotorSpeeds(0, 0);
    }
}
