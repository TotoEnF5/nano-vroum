using UnityEngine;

public class Piston : MonoBehaviour
{
    public AnimationCurve CurveMove;
    public float speed = 1;

    public GameObject PistonPlate;
    /// <summary>
    /// Ensure this is flat/same y compared to the plate
    /// </summary>
    public Transform Destination;

    private Vector3 m_startPosition;

    [SerializeField]
    private float m_animationTime = 0;
    private Vector3 m_destinationPosition;

    public float offset = 0;
    public float windup = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Might need to be local
        m_startPosition = transform.position;
        m_destinationPosition = Destination.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_animationTime = m_animationTime + Time.deltaTime * speed;

        transform.position = Vector3.Lerp(m_startPosition, m_destinationPosition, CurveMove.Evaluate(m_animationTime - offset - windup));
        if (m_animationTime >= 1 + offset + windup)
        {
            m_animationTime = offset;
            m_startPosition = transform.position;
        }
    }
}
