using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Button button;
    LevelSelectManager manager;
    public TextMeshProUGUI levelNameText, timeText, collectText;

    int levelIndex;

    public void SetData(LevelData data, LevelProgress progress, LevelSelectManager m)
    {
        levelNameText.text = data.levelName;
        manager = m;

        if (!progress.isUnlocked)
        {
            // Set colour to greyed out
            timeText.text = "-";
            collectText.text = "-";
            return;
        }

        // Set colour to normal

        // Set text to either best time or "-" based on if there is a best time
        timeText.text = progress.bestTime > 0 ? progress.bestTime.ToString() : "-";
        collectText.text = progress.starsEarned.ToString() + " / " + data.totalStars.ToString();

        button.onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        manager.LoadLevel(levelIndex);
    }
}
