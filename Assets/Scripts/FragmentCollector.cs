using UnityEngine;

public class FragmentCollector : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;

    [SerializeField] private int totalFragmentsThisLevel = 5; // change per level
    private int collectedCount = 0;

    public void SetTotalFragments(int count)
    {
        totalFragmentsThisLevel = count;
        collectedCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fragment"))
        {
            collectedCount++;
            Destroy(other.gameObject);

            Debug.Log($"Fragment collected! ({collectedCount}/{totalFragmentsThisLevel})");

            if (collectedCount >= totalFragmentsThisLevel)
            {
                puzzleManager.ShowPuzzle();
            }
        }
    }
}
