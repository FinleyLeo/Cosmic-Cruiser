using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public float timeTaken;
    public int levelIndex, flipsDone, collectedStars;
    public bool isUnlockedByDefault;
}
