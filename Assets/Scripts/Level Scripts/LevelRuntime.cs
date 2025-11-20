using TMPro;
using UnityEngine;

public class LevelRuntime : MonoBehaviour
{
    public bool isLevelActive = false;
    public float timer;
    public int starsCollected;

    [SerializeField] TextMeshProUGUI timerGUI;

    private void OnEnable()
    {
        // Makes isLevelActive true when level starts
        GameEvents.OnLevelStart += () => isLevelActive = true;
        GameEvents.OnGameWin += OnWin;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelStart -= () => isLevelActive = true;
        GameEvents.OnGameWin -= OnWin;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelActive)
        {
            timer += Time.deltaTime;
            timerGUI.text = timer.ToString("F2");
        }
    }

    public void AddCollectable()
    {
        starsCollected++;
    }

    public void OnWin()
    {
        OnLevelCompleted(timer, starsCollected);
    }

    public static void OnLevelCompleted(float timeTaken, int collectedStars)
    {
        LevelData data = LevelSelectManager.loadedLevelData;
        int levelIndex = data.levelIndex;

        LevelProgress progress = ProgressManager.Instance.LoadLevelProgress(levelIndex, data.isUnlockedByDefault);

        // Updates best time if better
        if (progress.bestTime <= 0 || timeTaken < progress.bestTime)
        {
            progress.bestTime = timeTaken;
        }

        if (collectedStars > progress.starsEarned)
        {
            progress.starsEarned = collectedStars;
        }

        if (levelIndex + 1 < LevelSelectManager.levelCount)
        {
            LevelProgress next = ProgressManager.Instance.LoadLevelProgress(levelIndex + 1, false);
            next.isUnlocked = true;
            ProgressManager.Instance.SaveLevelProgress(levelIndex + 1, next);
        }

        ProgressManager.Instance.SaveLevelProgress(levelIndex, progress);
    }
}
