using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] LevelData[] levels;
    public static int levelCount;
    bool buttonsCreated;

    [SerializeField] Transform buttonParent;
    [SerializeField] Transform[] levelButtons;
    [SerializeField] GameObject levelButtonPrefab;
    [SerializeField] LineRenderer lineRenderer;
    Vector3[] linePointsArray;

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

    private void Update()
    {
        if (buttonsCreated)
        {
            for (int i = 0; i < levelCount; i++)
            {
                lineRenderer.SetPosition(i, levelButtons[i].position);
            }
        }
    }

    void CreateLevelButtons()
    {
        linePointsArray = new Vector3[levelCount];
        levelButtons = new Transform[levelCount];

        for (int i = 0; i < levels.Length; i++)
        {
            LevelData levelData = levels[i];
            LevelProgress levelProgress = ProgressManager.Instance.LoadLevelProgress(i, levelData.isUnlockedByDefault);

            float ranX = Random.Range(-1.8f, 1.8f);
            float ranY = Random.Range(-0.1f, 0.1f);

            Vector3 spawnPos = new Vector2(ranX, (i * 2) - 3.5f + ranY);
            linePointsArray[i] = spawnPos;

            // position undecided
            GameObject buttonObj = Instantiate(levelButtonPrefab, spawnPos, Quaternion.identity);
            LevelButton button = buttonObj.GetComponent<LevelButton>();

            button.SetData(levelData, levelProgress, this);
            buttonObj.name = "Button " + i;
            buttonObj.transform.parent = buttonParent;

            levelButtons[i] = buttonObj.transform;
        }
        
        lineRenderer.positionCount = levelCount;
        buttonsCreated = true;
    }

    public void LoadLevel(int index)
    {
        loadedLevelData = levels[index];
        TransitionManager.instance.SwitchScene(index + 3); // Adjusts index for main menu, level and bootstrap
    }

    public void ExitSelectScreen()
    {
        TransitionManager.instance.SwitchScene("Main Menu");
    }
}
