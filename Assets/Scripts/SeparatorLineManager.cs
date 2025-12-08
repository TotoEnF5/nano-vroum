using System.Collections;
using UnityEngine;

public class SeparatorLineManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("LineRotator"))
        {
            StartCoroutine(RotateOverTime(Vector3.forward, 90, 3f));
        }
    }

    IEnumerator RotateOverTime(Vector3 axis, float angle, float duration)
    {
        // 1. Définir les rotations de départ et de fin
        Quaternion startRotation = transform.rotation;
        // Calcule la rotation cible en ajoutant l'angle à la rotation actuelle
        Quaternion endRotation = startRotation * Quaternion.AngleAxis(angle, axis);

        float timeElapsed = 0f;

        // 2. Boucle de rotation
        while (timeElapsed < duration)
        {
            // Calcule le ratio de progression (entre 0 et 1)
            float t = timeElapsed / duration;

            // Utilise Quaternion.Slerp pour interpoler la rotation en douceur
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // Met à jour le temps écoulé pour la prochaine frame
            timeElapsed += Time.deltaTime;

            // Attend la prochaine frame (le cœur de la coroutine)
            yield return null;
        }

        // 3. Assurez-vous que la rotation est EXACTEMENT à la fin
        transform.rotation = endRotation;
    }
}

