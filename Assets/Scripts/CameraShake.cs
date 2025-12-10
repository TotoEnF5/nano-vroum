using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0f;
    public float intensity = 0f;
    public bool resetLocalPosition;

    private void Update()
    {
        if (duration > 0f)
        {
            Vector2 random = Random.insideUnitCircle * intensity;
            transform.localPosition += new Vector3(random.x, random.y, 0f);
            duration -= Time.deltaTime;
        }
        else
        {
            if (resetLocalPosition)
            {
                transform.localPosition = Vector3.zero;
            }
            
            duration = 0f;
        }
    }

    public static void StartShake(float duration, float intensity)
    {
        if (Camera.main == null)
        {
            Debug.LogError("CameraShake.StartShake: No main camera defined!");
            return;
        }

        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        shake.duration = duration;
        shake.intensity = intensity;
    }
}
