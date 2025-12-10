using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(IA_Cursor))]
[RequireComponent(typeof(Rigidbody2D))]
public class Cursors : MonoBehaviour
{
    [Header("Cursor Movement")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Target Push (Impulse)")]
    [SerializeField] private float targetTravelTime = 10f;

    [SerializeField] private float pushForce = 10f;

    [Header("Inertia Control & Stop")]
    [SerializeField] private float destinationTolerance = 0.5f;
    [SerializeField] private float velocityReductionFactor = 0.1f;

    // --- Rotation de l'Objet Cible ---
    [Header("Target Rotation")]
    [SerializeField] private float minSpeedForRotation = 0.5f; 
    [SerializeField] private float rotationSpeed = 10f;

    private Vector2 moveInput;
    private GameObject target;
    private Rigidbody2D targetRB;
    private bool canAct = false;
    PlayerManager pm;
    ParticleSystem ps;
    public SpriteRenderer sr;
    private Camera mainCamera;

    public GameObject dashManager;

    private Rigidbody2D cursorRB;
    // Cette variable n'est plus utilisée pour la force simple
    private Vector2 targetDestination;

    public Tween scaleTween;
    public Tween rotateTween;
    private void Start()
    {
        pm = FindFirstObjectByType<PlayerManager>();
        ps = GetComponent<ParticleSystem>();
        ps.enableEmission = false;
        mainCamera = Camera.main;
        target = GameObject.FindGameObjectWithTag("Player");
        targetRB = target.GetComponent<Rigidbody2D>();
        pm.players.Add(gameObject);
        cursorRB = GetComponent<Rigidbody2D>();
        dashManager = GameObject.FindGameObjectWithTag("DashManager");
        dashManager.SetActive(false);
        //Center the cursor when spawning
        // targetDestination n'est plus nécessaire ici
        //Quelle galère
    }

    void Update()
    {
        ps.startColor = sr.color;
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        ClampPositionToScreen();
    }

    private void FixedUpdate()
    {
        if (cursorRB != null)
        {
            // Calculer le mouvement désiré.
            Vector2 movementVector = moveInput * moveSpeed;

            // Appliquer la vélocité.
            // Cela déplace le Rigidbody2D dans le FixedUpdate, le moment correct
            // pour les manipulations de physique.
            cursorRB.linearVelocity = movementVector;
        }
        AlignRotationWithVelocity();
    }
    private void AlignRotationWithVelocity()
    {
        if (targetRB == null) return;

        // Récupérer la vélocité du Rigidbody
        Vector2 velocity = targetRB.linearVelocity;

        // 1. Vérifier si la vitesse est suffisante pour déclencher la rotation
        if (velocity.sqrMagnitude > minSpeedForRotation * minSpeedForRotation)
        {
            // 2. Calculer l'angle cible (en degrés)
            // La fonction Atan2 donne l'angle en radians entre l'axe X et le vecteur (velocity.x, velocity.y)
            float targetAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

            // Si votre sprite est orienté vers le haut (axe Y) par défaut, vous pouvez ajouter ou soustraire 90 degrés ici:
            // targetAngle -= 90f; 

            // 3. Créer la rotation cible
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

            // 4. Interpoler (Slerp) la rotation actuelle vers la rotation cible
            target.transform.rotation = Quaternion.Slerp(
                target.transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void PushTargetToCursor(CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (canAct)
        {   

            Vector2 startPos = targetRB.position;
            Vector2 endPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 pushDirection = (endPos - startPos).normalized;

            float distance = Vector2.Distance(startPos, endPos);

            // 1. Calculer la vitesse initiale cible (v_cible = Distance / Temps_cible)
            float targetSpeed = distance / targetTravelTime;


            float requiredImpulseMagnitude = targetRB.mass * targetSpeed;

            Vector2 requiredImpulse = pushDirection * requiredImpulseMagnitude;


            targetDestination = endPos;

            // 2. Annuler toute vélocité précédente et appliquer l'impulsion CALCULÉE
            targetRB.linearVelocity = Vector2.zero;
            targetRB.AddForce(requiredImpulse, ForceMode2D.Impulse); // Utilisation de l'impulsion calculée

            // 3. Jouer le son de dash
            dashManager.SetActive(true);

            // 4. Mettre fin au tour (canAct = false) et appeler pm.EndTurn()
            canAct = false;
            pm.EndTurn();
            dashManager.SetActive(false);
            // La durée du DOTween est maintenant simplement le targetTravelTime
            DOTween.Sequence()
                // On utilise targetTravelTime comme durée d'attente
                .AppendInterval(targetTravelTime * 1.05f)
                .AppendCallback(CheckAndReduceVelocity)
                .Play();
        }
        if(scaleTween != null)
        {
            scaleTween.Kill();
        }

        if(rotateTween != null)
        {
            rotateTween.Kill();
        }
        rotateTween = sr.transform.DOPunchRotation(new Vector3(sr.transform.eulerAngles.x, sr.transform.eulerAngles.y, sr.transform.eulerAngles.z + 360), 0.8f, 1);
        scaleTween = sr.transform.DOPunchScale(Vector3.one * 1.05f, .3f, 1);
        scaleTween.onKill += ()=> { sr.transform.DOScale(Vector3.one,0.2f); };
        
    }

    private void CheckAndReduceVelocity()
    {
        if (targetRB == null) return;
        if (Vector2.Distance(targetRB.position, targetDestination) <= destinationTolerance)
        {
            Vector2 currentVelocity = targetRB.linearVelocity;

            targetRB.linearVelocity = currentVelocity * velocityReductionFactor;
        }
    }

    public void SetCanAct(bool status)
    {
        canAct = status;
        if (canAct)
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
        if (mainCamera == null)
        {
            return;
        }

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
