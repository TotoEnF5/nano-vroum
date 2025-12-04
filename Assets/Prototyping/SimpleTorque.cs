using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class SimpleTorque: MonoBehaviour
{
    public bool ActivateWhenCloseToCamera = false;
    public float torque = 1;
    private Rigidbody2D m_rigidbody2D;
    private bool m_wokenUp = false;
    public float wakeRadius = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        if (ActivateWhenCloseToCamera)
        {
            m_rigidbody2D.simulated = false;
            print("I sleep");
        }
    }
    private void Update()
    {

        m_rigidbody2D.AddTorque(torque);
        if (!m_wokenUp && ActivateWhenCloseToCamera)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPos.x >= 0 && viewPos.x <= wakeRadius && viewPos.y >= 0 && viewPos.y <= wakeRadius)
            {
                m_wokenUp = true;
                m_rigidbody2D.simulated = true;
            }
        }


    }

}
