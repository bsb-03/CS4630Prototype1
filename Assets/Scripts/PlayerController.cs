using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{
    public Transform resetPoint;

    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private float acceleration = 10.0f;
    private float maxSpeed = 4f;

    private Vector3 floorNormal = Vector3.up;
    public float raycastDistance = 2.0f;
    public LayerMask groundLayer;

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

        moveInput = new Vector2(horizontal, vertical);
    }

    void HandlePlayerMove()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        movement = movement.normalized;
        movement = Vector3.ProjectOnPlane(movement, floorNormal);

        Vector3 currentVel = rb.linearVelocity;
        currentVel.y = 0;

        if (currentVel.magnitude < maxSpeed)
        {
            rb.AddForce(movement * acceleration, ForceMode.Acceleration);
        }
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, raycastDistance, groundLayer))
        {
            floorNormal = hit.normal;
        }
        else
        {
            floorNormal = Vector3.up; // Default to flat world plane when airborne
        }

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            HandlePlayerMove();
        }
    }

    public void ResetPlayer()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = resetPoint.position;
        rb.rotation = resetPoint.rotation;
    }
}
