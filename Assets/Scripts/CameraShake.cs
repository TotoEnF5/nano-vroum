using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0f;
    public float intensity = 0f;
    public bool resetLocalPosition;

    public void Update()
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
}
