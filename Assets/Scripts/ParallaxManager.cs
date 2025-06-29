using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public RectTransform rectTransform;
        public float parallaxFactor = 0.1f; // Higher = moves more
    }

    public ParallaxLayer[] layers;
    public float smoothing = 5f;

    private Vector2 screenCenter;

    private void Start()
    {
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    private void Update()
    {
        Vector2 mouseOffset = (Vector2)Input.mousePosition - screenCenter;
        mouseOffset /= screenCenter; // Normalize to -1 to 1

        foreach (var layer in layers)
        {
            if (layer.rectTransform == null) continue;

            Vector2 targetPos = new Vector2(
                mouseOffset.x * layer.parallaxFactor * 50f, // tweak multiplier as needed
                mouseOffset.y * layer.parallaxFactor * 50f
            );

            layer.rectTransform.anchoredPosition = Vector2.Lerp(
                layer.rectTransform.anchoredPosition,
                targetPos,
                Time.deltaTime * smoothing
            );
        }
    }
}
