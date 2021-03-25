using UnityEngine;
using Mirror;

public class PlayerMovementController : NetworkBehaviour
{
    [Header("Required")]
    public Animator animator;

    [SerializeField] private float movementSpeed;
    [SerializeField] private CharacterController controller = null;

    private Controls controls;
    private bool running;
    private bool isMovingBackward;
    public bool isGrounded;
    private float gravityValue = -20.81f;
    private float jumpHeight = 8.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    public override void OnStartAuthority()
    {
        enabled = true;
    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();
    [ClientCallback]
    private void Update() => Move();

    [Client]
    private void Move()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            movementSpeed = (running) ? (isMovingBackward) ? 5f : 8f : 5f;
            moveDirection *= movementSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpHeight;

        }

        moveDirection.y += gravityValue * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        CheckMovementAnimationToSend();
        CheckAttackAnimationToSend();
    }

    private void CheckAttackAnimationToSend()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("mouseClicked", true);
        } else
        {
            animator.SetBool("mouseClicked", false);
        }
    }

    private void CheckMovementAnimationToSend()
    {
        running = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("running", running);
        isMovingBackward = false;

        if (Input.GetKey(KeyCode.W))
        {
            if (running)
            {
                animator.SetFloat("forward", 1f);
                animator.SetFloat("left", 0f);
                animator.SetFloat("right", 0f);
                animator.SetFloat("backward", 0f);
                animator.SetBool("moving", false);
            }
            else
            {
                animator.SetFloat("forward", 1f);
                animator.SetFloat("left", 0f);
                animator.SetFloat("right", 0f);
                animator.SetFloat("backward", 0f);
                animator.SetBool("moving", true);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetFloat("left", 1f);
            animator.SetFloat("forward", 0f);
            animator.SetFloat("right", 0f);
            animator.SetFloat("backward", 0f);
            animator.SetBool("moving", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetFloat("right", 1f);
            animator.SetFloat("forward", 0f);
            animator.SetFloat("left", 0f);
            animator.SetFloat("backward", 0f);
            animator.SetBool("moving", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isMovingBackward = true;

            animator.SetFloat("backward", 1f);
            animator.SetFloat("forward", 0f);
            animator.SetFloat("left", 0f);
            animator.SetFloat("right", 0f);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetFloat("forward", 0f);
            animator.SetFloat("left", 0f);
            animator.SetFloat("right", 0f);
            animator.SetFloat("backward", 0f);
            animator.SetBool("moving", false);
        }
    }
}
