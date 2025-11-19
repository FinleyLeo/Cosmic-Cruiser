using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public LevelProgress LoadLevelProgress(int levelIndex, bool unlockedByDefault)
    {
        LevelProgress lp = new LevelProgress();

        lp.bestTime = PlayerPrefs.GetFloat($"level_{levelIndex}_bestTime", -1f);
        lp.starsEarned = PlayerPrefs.GetInt($"level_{levelIndex}_bestCollects", 0);

        // unlocked defaults to SO value if no PlayerPrefs entry exists yet
        lp.isUnlocked = PlayerPrefs.GetInt($"level_{levelIndex}_unlocked", unlockedByDefault ? 1 : 0) == 1;

        return lp;
    }

    public void SaveLevelProgress(int levelIndex, LevelProgress lp)
    {
        PlayerPrefs.SetFloat($"level_{levelIndex}_bestTime", lp.bestTime);
        PlayerPrefs.SetInt($"level_{levelIndex}_bestCollects", lp.starsEarned);
        PlayerPrefs.SetInt($"level_{levelIndex}_unlocked", lp.isUnlocked ? 1 : 0);

        PlayerPrefs.Save();
    }

}
