using DG.Tweening;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


[RequireComponent(typeof(IA_Cursor))]
public class Cursors : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float pushForce = 10f;
    private Vector2 moveInput;
    private GameObject target;
    private Rigidbody targetRB;
    private bool canAct = false;
    PlayerManager pm;
    ParticleSystem ps;
    SpriteRenderer sr;

    private void Start()
    {
        pm = FindFirstObjectByType<PlayerManager>();
        ps = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        ps.enableEmission = false;
        target = GameObject.FindGameObjectWithTag("target");
        targetRB = target.GetComponent<Rigidbody>();
        pm.players.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        ps.startColor = sr.color;
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void TeleportObjectByTag()
    {
        if (canAct)
        {
            Vector3 startPos = targetRB.position;
            Vector3 endPos = transform.position;
            Vector3 pushDirection = (endPos - startPos).normalized;
            targetRB.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            pm.EndTurn();

        }
    }
    public void SetCanAct(bool status)
    {
        canAct = status;
        if(canAct)
        {
            ps.enableEmission = true;
        }
    }
}