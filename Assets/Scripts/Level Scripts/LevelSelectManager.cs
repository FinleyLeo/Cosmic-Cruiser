using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] LevelData[] levels;
    [SerializeField] GameObject levelButtonPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateLevelButtons()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            Vector3 spawnPos = new Vector2(0, i);

            // position undecided
            GameObject button = Instantiate(levelButtonPrefab, spawnPos, Quaternion.identity);
        }
    }
}
