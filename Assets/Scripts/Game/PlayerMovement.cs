using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Input System")]
    private InputSystem_Actions controls;

    [Header("Side Character")]
    [SerializeField] private GameObject sideCharacterPrefab;
    [SerializeField] private float sideDistance = 0.6f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float sideCheckRadius = 0.2f;
    private GameObject sideCharacter;

    private Vector2 facingDir = Vector2.down; // default facing down

    private void Awake()
    {
        controls = new InputSystem_Actions();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (sideCharacterPrefab != null)
        {
            sideCharacter = Instantiate(sideCharacterPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Side character prefab not assigned!");
        }
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 movement = moveInput.normalized;

        // Player movement
        rb.linearVelocity = movement * moveSpeed;

        // Update facing direction if moving
        if (movement != Vector2.zero)
        {
            facingDir = movement;

            float angle = 0f;

            if (facingDir == Vector2.up)
                angle = 0f;
            else if (facingDir == Vector2.left)
                angle = 90f;
            else if (facingDir == Vector2.down)
                angle = 180f;
            else if (facingDir == Vector2.right)
                angle = -90f;

            rb.rotation = angle;
        }


        UpdateSideCharacter();
    }

    private void UpdateSideCharacter()
    {
        if (sideCharacter == null) return;

        Vector2 leftDir = new Vector2(-facingDir.y, facingDir.x).normalized;
        Vector2 rightDir = -leftDir;

        Vector3 leftTarget = transform.position + (Vector3)(leftDir * sideDistance);
        Vector3 rightTarget = transform.position + (Vector3)(rightDir * sideDistance);

        if (!Physics2D.OverlapCircle(leftTarget, sideCheckRadius, obstacleLayer))
        {
            sideCharacter.transform.position = Vector3.Lerp(sideCharacter.transform.position, leftTarget, 0.2f);
        }
        else if (!Physics2D.OverlapCircle(rightTarget, sideCheckRadius, obstacleLayer))
        {
            sideCharacter.transform.position = Vector3.Lerp(sideCharacter.transform.position, rightTarget, 0.2f);
        }

        sideCharacter.transform.rotation = Quaternion.Euler(0, 0, rb.rotation);
    }

}
