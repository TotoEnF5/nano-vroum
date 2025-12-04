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
    private Rigidbody2D targetRB;
    private bool canAct = false;
    PlayerManager pm;
    ParticleSystem ps;
    SpriteRenderer sr;

    private Camera mainCamera;

    private void Start()
    {
        pm = FindFirstObjectByType<PlayerManager>();
        ps = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        ps.enableEmission = false;
        mainCamera = Camera.main;
        target = GameObject.FindGameObjectWithTag("Player");
        targetRB = target.GetComponent<Rigidbody2D>();
        pm.players.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        ps.startColor = sr.color;
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        ClampPositionToScreen();
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
            targetRB.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
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
        else
        {
            ps.enableEmission = false;
        }
    }

    private void ClampPositionToScreen()
    {


        // Convertit les coins de l'écran (0,0 et 1,1 en viewport coordinates) en World Position
        Vector3 minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - mainCamera.transform.position.z));
        Vector3 maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - mainCamera.transform.position.z));

        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(
            currentPosition.x,
            minScreenBounds.x,
            maxScreenBounds.x
        );

        currentPosition.y = Mathf.Clamp(
            currentPosition.y,
            minScreenBounds.y,
            maxScreenBounds.y
        );

        transform.position = currentPosition;
    }
}