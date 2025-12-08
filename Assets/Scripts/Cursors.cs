using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(IA_Cursor))]
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
    SpriteRenderer sr;
    private Camera mainCamera;

    public SpriteRenderer Image1;
    public SpriteRenderer Image2;
    public SpriteRenderer toClampTo;
    // Cette variable n'est plus utilisée pour la force simple
    private Vector2 targetDestination;

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
<<<<<<< Updated upstream
        Image1 = GameObject.FindGameObjectWithTag("Border1").GetComponent<SpriteRenderer>();
        Image2 = GameObject.FindGameObjectWithTag("Border2").GetComponent<SpriteRenderer>();
        toClampTo = GameObject.FindGameObjectWithTag("Border1").GetComponent<SpriteRenderer>();
        //Center the cursor when spawning
        // targetDestination n'est plus nécessaire ici
=======

>>>>>>> Stashed changes
    }

    void Update()
    {
        ps.startColor = sr.color;
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        ClampPositionToScreen();
    }

    private void FixedUpdate()
    {
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

    public void PushTargetToCursor()
    {
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

            // 3. Mettre fin au tour (canAct = false) et appeler pm.EndTurn()
            canAct = false;
            pm.EndTurn();


            // La durée du DOTween est maintenant simplement le targetTravelTime
            DOTween.Sequence()
                // On utilise targetTravelTime comme durée d'attente
                .AppendInterval(targetTravelTime * 1.05f)
                .AppendCallback(CheckAndReduceVelocity)
                .Play();
        }
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
    public void SetClamp(int playerIndex)
    {
        if(playerIndex == 0)
        {
            toClampTo = Image1;

        }
        else
        {
            toClampTo = Image2;
        }
    }

    private void ClampPositionToScreen()
    {
        Vector3 min = new Vector3( toClampTo.transform.position.x - toClampTo.size.x / 2f, toClampTo.transform.position.y - toClampTo.size.y / 2f);
        Vector3 max = new Vector3(toClampTo.transform.position.x + toClampTo.size.x / 2f, toClampTo.transform.position.y + toClampTo.size.y / 2f);

 

        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(
            currentPosition.x,
            min.x,
            max.x
        );

        currentPosition.y = Mathf.Clamp(
            currentPosition.y,
            min.y,
            max.y
        );
        transform.position = currentPosition;
    }
}