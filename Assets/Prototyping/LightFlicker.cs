using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightFlicker : MonoBehaviour
{
    public Light2D Light;
    public AnimationCurve CurveLighting;
    public float speed = 1;
    //lerps from 0-1 with the curveRotate 
    private float LightingIntensityStart;
    public float LightingIntensityIncrement = 1;

    [SerializeField]
    private float m_lightingTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Light = GetComponent<Light2D>();
        LightingIntensityStart = Light.intensity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_lightingTime = m_lightingTime + Time.deltaTime * speed;

        Light.intensity = Mathf.Lerp(LightingIntensityStart, LightingIntensityIncrement + LightingIntensityStart, CurveLighting.Evaluate(m_lightingTime));
        if (m_lightingTime >= 1)
        {
            m_lightingTime = 0;
            LightingIntensityStart = Light.intensity;
        }
    }
}
