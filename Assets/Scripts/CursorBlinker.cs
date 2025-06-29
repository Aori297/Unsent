using UnityEngine;
using UnityEngine.UI;

public class CursorBlinker : MonoBehaviour
{
    [SerializeField] private float blinkRate = 0.5f;

    private Image image;
    private float timer;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer >= blinkRate)
        {
            image.enabled = !image.enabled;
            timer = 0f;
        }
    }
}
