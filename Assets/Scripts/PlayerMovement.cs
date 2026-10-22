using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private bool hasJumpInput = false;

    void Start()

    {
        rb = GetComponent<Rigidbody>();

    }



    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.aKey.isPressed)
            horizontal = -1f;

        if (Keyboard.current.dKey.isPressed)
            horizontal = 1f;

        if (Keyboard.current.sKey.isPressed)
            vertical = -1f;

        if (Keyboard.current.wKey.isPressed)
            vertical = 1f;
        if (Keyboard.current.spaceKey.isPressed)
            hasJumpInput = true;

        moveInput = new Vector2(horizontal, vertical);
    }

    void HandlePlayerMove()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);

        movement = movement.normalized;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void HandleJump()
    {
        rb.AddForce(new Vector3(0f,0.5f,0f), ForceMode.Impulse);
        hasJumpInput = false;
    }

    void FixedUpdate()
    {
        HandlePlayerMove();

        if(hasJumpInput)
        {
            HandleJump();
        }  
    }
}
