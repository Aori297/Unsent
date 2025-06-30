using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public string pieceID;

    private Vector3 offset;
    private Vector3 startPosition;
    private bool isDragging = false;
    private bool isSnapped = false;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (isSnapped) return;

        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (isSnapped || !isDragging) return;

        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        if (isSnapped || !isDragging) return;

        isDragging = false;

        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("PuzzleMold"))
            {
                PuzzleSlot mold = hit.GetComponent<PuzzleSlot>();
                if (mold != null && mold.moldID == pieceID)
                {
                    transform.position = mold.transform.position;
                    isSnapped = true;
                    Debug.Log("Snapped into place.");
                    return;
                }
            }
        }

        transform.position = startPosition;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}
