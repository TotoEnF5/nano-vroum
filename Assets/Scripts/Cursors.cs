using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


[RequireComponent(typeof(IA_Cursor))]
public class Cursors : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    private Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

}