using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

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
    private Vector3 m_startScale;

    //We will alter the x scale of that guy
    public GameObject PistonTige;
    public GameObject Head;
     
    [SerializeField]
    private float m_animationTime = 0;
    private Vector3 m_destinationPosition;

    public float offset = 0;
    public float windup = 0;
    public float PistonSize;

    public bool particlesDisabled = false;
    public List<ParticleSystem> bubbles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Might need to be local
        m_startPosition = Head.transform.position;
        m_startScale = transform.localScale;
        m_destinationPosition = Destination.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float evaluatedValue = CurveMove.Evaluate(m_animationTime - offset - windup);
        m_animationTime = m_animationTime + Time.deltaTime * speed * GamestateManager.Instance.GlobalTime;
        PistonTige.transform.localScale = Vector3.Lerp(m_startScale, m_startScale + new Vector3(1,0,0) * PistonSize, evaluatedValue);
        Head.transform.position = Vector3.Lerp(m_startPosition, m_destinationPosition, evaluatedValue);
        if(evaluatedValue >= 0.6f + offset + windup)
        {
            Head.tag = "killer";
        }
        else
        {
            Head.tag = "Untagged";
        }

        //Disable the particles on the way back of the piston
        if(evaluatedValue >= 0.95f && !particlesDisabled)
        {
            particlesDisabled = true;
            SetParticles();
        }
        else if(evaluatedValue < 0.01f && particlesDisabled)
        {
            particlesDisabled = false;
            SetParticles();
        }

        if (m_animationTime >= 1 + offset + windup)
        {
            m_animationTime = offset;
            m_startPosition = Head.transform.position;
            m_startScale = PistonTige.transform.localScale;
        }
    }

    public void SetParticles()
    { 
        foreach(ParticleSystem ps in bubbles)
        {
            EmissionModule emission = ps.emission;
            if (particlesDisabled)
            {
                emission.enabled = false;
            }
            else
            {
                emission.enabled = true;
            }

        }
    }
}
