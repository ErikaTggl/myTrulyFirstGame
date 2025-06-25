using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public PlayerInputActions playerActions;
    public Vector2 moveInput;
    public float movementSpeed = 5f;

    public Rigidbody2D rb;
    private float jumpForce = 3f;
    private bool isGrounded = true;
    public Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
    }
    private void playerOnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void playerOnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }   

    private void Awake()
    {
        playerActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (playerActions == null)
            playerActions = new PlayerInputActions();

        playerActions.Enable();
        playerActions.Movement.Move.performed += playerOnMove;
        playerActions.Movement.Move.canceled += ctx => moveInput = Vector2.zero;
        playerActions.Movement.Jump.performed += playerOnJump;
      
    }

    private void OnDisable()
    {
        playerActions.Disable();
        playerActions.Movement.Move.Disable();
        playerActions.Movement.Jump.Disable();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(moveInput * Time.fixedDeltaTime * movementSpeed);
    }
}
