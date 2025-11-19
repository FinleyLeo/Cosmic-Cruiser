using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] LevelData[] levels;
    public static int levelCount;

    [SerializeField] Transform buttonParent;
    [SerializeField] GameObject levelButtonPrefab;

    public static LevelData loadedLevelData;

    private void Awake()
    {
        levelCount = levels.Length;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateLevelButtons();
    }

    void CreateLevelButtons()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            LevelData levelData = levels[i];
            LevelProgress levelProgress = ProgressManager.Instance.LoadLevelProgress(i, levelData.isUnlockedByDefault);

            Vector3 spawnPos = new Vector2(0, i);

            // position undecided
            GameObject buttonObj = Instantiate(levelButtonPrefab, spawnPos, Quaternion.identity);
            LevelButton button = buttonObj.GetComponent<LevelButton>();

            button.SetData(levelData, levelProgress, this);
            buttonObj.name = "Button " + i;
        }
    }

    public void LoadLevel(int index)
    {
        loadedLevelData = levels[index];
        TransitionManager.instance.SwitchScene(index, 1f);
    }
}
