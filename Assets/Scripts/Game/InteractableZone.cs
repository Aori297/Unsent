using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableZone : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject interactPrompt;

    private bool playerInRange = false;
    private GameObject player;

    [SerializeField] public float hoverOffsetY = 4f;
    [SerializeField] public float followSmoothness = 5f;

    private void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;

            if (interactPrompt != null)
                interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;

            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (teleportTarget != null && player != null)
            {
                player.transform.position = teleportTarget.position;
                if (interactPrompt != null)
                    interactPrompt.SetActive(false);
            }
        }
    }
    private void LateUpdate()
    {
        if (playerInRange && interactPrompt != null && player != null)
        {
            Vector3 targetPos = player.transform.position + new Vector3(0f, hoverOffsetY, 0f);
            interactPrompt.transform.position = Vector3.Lerp(interactPrompt.transform.position, targetPos, Time.deltaTime * followSmoothness);
        }
    }

}
