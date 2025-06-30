using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform[] menuButtons;
    public GameObject goodbyeImage;

    [Header("Animation Settings")]
    public float buttonDisappearDelay = 0.15f;
    public float buttonDisappearDuration = 0.2f;
    public float goodbyePopDuration = 0.35f;

    private void Start()
    {
        goodbyeImage.SetActive(false);
        goodbyeImage.transform.localScale = Vector3.zero;
    }

    public void OnPlayPressed()
    {
        int lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        SceneManager.LoadScene("Memory " + lastLevel);
    }

    public void OnLevelSelectPressed()
    {
    
        SceneManager.LoadScene("Level Select");
    }

    public void OnSettingsPressed()
    {
        Debug.Log("Settings pressed - implement your pan or panel here");
    }

    public void OnQuitPressed()
    {
        StartCoroutine(QuitSequence());
    }

    private IEnumerator QuitSequence()
    {
        foreach (var btn in menuButtons)
        {
            yield return ScalePopOut(btn);
            btn.gameObject.SetActive(false);
            yield return new WaitForSeconds(buttonDisappearDelay);
        }

        goodbyeImage.SetActive(true);
        yield return ScalePopIn(goodbyeImage.transform);

        yield return new WaitForSeconds(1.2f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private IEnumerator ScalePopOut(RectTransform rect)
    {
        float t = 0f;
        Vector3 start = rect.localScale;
        Vector3 end = Vector3.zero;

        while (t < buttonDisappearDuration)
        {
            t += Time.deltaTime;
            rect.localScale = Vector3.Lerp(start, end, t / buttonDisappearDuration);
            yield return null;
        }

        rect.localScale = end;
    }

    private IEnumerator ScalePopIn(Transform trans)
    {
        float t = 0f;
        float duration = goodbyePopDuration;

        Vector3 startScale = Vector3.zero;
        Vector3 overshoot = Vector3.one * 1.2f;
        Vector3 undershoot = Vector3.one * 0.95f;
        Vector3 final = Vector3.one;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            if (progress < 0.4f)
                trans.localScale = Vector3.Lerp(startScale, overshoot, progress / 0.4f);
            else if (progress < 0.7f)
                trans.localScale = Vector3.Lerp(overshoot, undershoot, (progress - 0.4f) / 0.3f);
            else
                trans.localScale = Vector3.Lerp(undershoot, final, (progress - 0.7f) / 0.3f);

            yield return null;
        }

        trans.localScale = final;
    }
}
