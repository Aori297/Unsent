using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject puzzleScreen; // Your panel
    [SerializeField] private GameObject puzzleContainer; // All pieces parented under this

    public void ShowPuzzle()
    {
        puzzleScreen.SetActive(true);
        puzzleContainer.SetActive(true);
    }
}
