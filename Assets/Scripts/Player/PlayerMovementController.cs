using UnityEngine;
using Mirror;

public class PlayerMovementController : NetworkBehaviour
{
    [Header("Required")]
    public Animator animator;

    [SerializeField] private float movementSpeed;
    [SerializeField] private CharacterController controller = null;

    private Vector2 previousInput;
    private Controls controls;
    private bool running;
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
        Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        Controls.Player.Move.canceled += ctx => ResetMovement();
    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();
    [ClientCallback]
    private void Update() => Move();

    [Client]
    private void SetMovement(Vector2 movement) => previousInput = movement;

    [Client]
    private void ResetMovement() => previousInput = Vector2.zero;

    [Client]
    private void Move()
    {
        CheckAnimationToSend();

        movementSpeed = (running) ? 8f : 5f;

        Vector3 right = controller.transform.right;
        Vector3 forward = controller.transform.forward;
        right.y = 0f;
        forward.y = 0f;

        Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;
        controller.Move(movement * movementSpeed * Time.deltaTime);
    }

    private void CheckAnimationToSend()
    {
        running = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("running", running);

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
