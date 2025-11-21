using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Button button;
    LevelSelectManager manager;
    public TextMeshProUGUI levelNameText, timeText;
    public Sprite starOn, starOff;
    public Image[] stars;

    int levelIndex;

    public void SetData(LevelData data, LevelProgress progress, LevelSelectManager m)
    {
        levelNameText.text = data.levelName;
        manager = m;

        if (!progress.isUnlocked)
        {
            // Set colour to greyed out
            timeText.text = "-";
            button.enabled = false;
            return;
        }

        // Set colour to normal

        // Set text to either best time or "-" based on if there is a best time
        timeText.text = progress.bestTime > 0 ? progress.bestTime.ToString() : "-";

        for (int i = 0; i < progress.starsEarned; i++)
        {
            stars[i].sprite = starOn;
            stars[i].color = Color.yellow;
        }

        levelIndex = data.levelIndex;
        button.onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        manager.LoadLevel(levelIndex);
    }
}
