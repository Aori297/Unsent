using UnityEngine;
using TMPro;

public class LevelNameRevealer : MonoBehaviour
{
    public bool revealEnglish = false;
    [System.Serializable]
    public class LevelEntry
    {
        public TMP_Text levelText;
        public string glyphText;
        public string englishText;
    }

    public LevelEntry[] levels;

    private void Start()
    {
        foreach (var entry in levels)
        {
            entry.levelText.text = revealEnglish ? entry.englishText : entry.glyphText;
        }
    }

    public void RevealEnglishNames()
    {
        revealEnglish = true;
        foreach (var entry in levels)
        {
            entry.levelText.text = entry.englishText;
        }
    }
}
