using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] string levelName;
    [SerializeField] float timeTaken;
    [SerializeField] int levelIndex, flipsDone, collectedStars;
    [SerializeField] bool isUnlockedByDefault;
}
