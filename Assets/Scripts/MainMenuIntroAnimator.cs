using System.Collections;
using UnityEngine;
using TMPro;

public class MainMenuIntroAnimator : MonoBehaviour
{
    public TMP_Text textUn;
    public TMP_Text textSent;
    public TMP_Text textDot;
    public TMP_Text textFromMeToYou;

    public GameObject menuButtonsGroup;

    public float fadeDuration = 0.5f;
    public float delayBetween = 0.4f;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        // Hide all texts at start
        HideTextInstantly(textUn);
        HideTextInstantly(textSent);
        HideTextInstantly(textDot);
        HideTextInstantly(textFromMeToYou);
        menuButtonsGroup.SetActive(false);

        yield return FadeIn(textUn);
        yield return new WaitForSeconds(delayBetween);

        yield return FadeIn(textSent);
        yield return new WaitForSeconds(delayBetween);

        yield return FadeIn(textDot);
        yield return new WaitForSeconds(delayBetween + 0.3f);

        yield return FadeIn(textFromMeToYou);
        yield return new WaitForSeconds(delayBetween + 0.5f);

        menuButtonsGroup.SetActive(true);
    }

    void HideTextInstantly(TMP_Text tmp)
    {
        var color = tmp.color;
        color.a = 0f;
        tmp.color = color;
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
}
