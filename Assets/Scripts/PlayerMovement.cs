using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject sideCharacterPrefab; // drag your prefab here
    [SerializeField] private Vector2 sideOffset = new Vector2(1.5f, 0f); // adjust this as needed

    private Vector2 moveInput;
    private Rigidbody2D rb;

    private InputSystem_Actions controls;
    private GameObject sideCharacter;

    private void Awake()
    {
        controls = new InputSystem_Actions();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (sideCharacterPrefab != null)
        {
            sideCharacter = Instantiate(sideCharacterPrefab, transform.position + (Vector3)sideOffset, Quaternion.identity);
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
        rb.linearVelocity = moveInput.normalized * moveSpeed;

        // Update side character's position with offset
        if (sideCharacter != null)
        {
            sideCharacter.transform.position = transform.position + (Vector3)sideOffset;
        }
    }
}
