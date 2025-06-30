using System.Collections;
using UnityEngine;
using TMPro;

public class MainMenuIntroAnimator : MonoBehaviour
{
    [Header("Intro Texts")]
    public TMP_Text textUn;
    public TMP_Text textSent;
    public TMP_Text textDot;
    public TMP_Text textFromMeToYou;

    [Header("Message Shell & Buttons")]
    public CanvasGroup messageShellGroup;
    public RectTransform[] menuButtons;
    public RectTransform buttonContainer;

    [Header("Animation Settings")]
    public float fadeDuration = 0.5f;
    public float delayBetween = 0.4f;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        // Hide intro texts
        HideTextInstantly(textUn);
        HideTextInstantly(textSent);
        HideTextInstantly(textDot);
        HideTextInstantly(textFromMeToYou);

        // Hide shell
        messageShellGroup.alpha = 0f;
        messageShellGroup.gameObject.SetActive(false);

        // Hide buttons
        foreach (var btn in menuButtons)
        {
            btn.gameObject.SetActive(false);
            btn.localScale = Vector3.zero;
        }

        // Fade in texts
        yield return FadeIn(textUn);
        yield return new WaitForSeconds(delayBetween);

        yield return FadeIn(textSent);
        yield return new WaitForSeconds(delayBetween);

        yield return FadeIn(textDot);
        yield return new WaitForSeconds(delayBetween + 0.3f);

        yield return FadeIn(textFromMeToYou);
        yield return new WaitForSeconds(delayBetween + 0.5f);

        // Fade in shell
        messageShellGroup.gameObject.SetActive(true);
        yield return FadeCanvasGroup(messageShellGroup, 0f, 1f, 0.5f);

        // Pop-in each button
        for (int i = 0; i < menuButtons.Length; i++)
        {
            RectTransform btn = menuButtons[i];
            btn.gameObject.SetActive(true);
            btn.localScale = Vector3.zero;
            yield return ScalePopIn(btn);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator FadeIn(TMP_Text tmp)
    {
        tmp.gameObject.SetActive(true);
        float t = 0f;
        Color color = tmp.color;
        color.a = 0f;
        tmp.color = color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            tmp.color = color;
            yield return null;
        }

        color.a = 1f;
        tmp.color = color;
    }

    IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration)
    {
        float t = 0f;
        group.alpha = from;

        while (t < duration)
        {
            t += Time.deltaTime;
            group.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }

        group.alpha = to;
    }

    IEnumerator ScalePopIn(RectTransform rect)
    {
        float t = 0f;
        float duration = 0.35f;

        Vector3 startScale = Vector3.zero;
        Vector3 overshoot = Vector3.one * 1.2f;
        Vector3 undershoot = Vector3.one * 0.95f;
        Vector3 final = Vector3.one;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            if (progress < 0.4f)
            {
                rect.localScale = Vector3.Lerp(startScale, overshoot, progress / 0.4f);
            }
            else if (progress < 0.7f)
            {
                rect.localScale = Vector3.Lerp(overshoot, undershoot, (progress - 0.4f) / 0.3f);
            }
            else
            {
                rect.localScale = Vector3.Lerp(undershoot, final, (progress - 0.7f) / 0.3f);
            }

            yield return null;
        }

        rect.localScale = final;
    }


    void HideTextInstantly(TMP_Text tmp)
    {
        var color = tmp.color;
        color.a = 0f;
        tmp.color = color;
        tmp.gameObject.SetActive(false);
    }
}
