using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public AnimationCurve CurveRotate;
    public float speed = 1;
    //lerps from 0-1 with the curveRotate 
    private Vector3 startingRotation;
    [Range(-360,360)]
    public float AngleIncrement = 360f;


    [SerializeField]
    private float m_rotationTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_rotationTime = m_rotationTime + Time.deltaTime * speed;
        
        float rotationStrength = CurveRotate.Evaluate(m_rotationTime);
        transform.eulerAngles = Vector3.Lerp(startingRotation, startingRotation + new Vector3(0, 0, AngleIncrement), CurveRotate.Evaluate(m_rotationTime));
        if (m_rotationTime >= 1)
        {
            m_rotationTime = 0;
            startingRotation = transform.eulerAngles;
        }
    }
}
