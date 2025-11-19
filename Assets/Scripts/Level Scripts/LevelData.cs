using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName, sceneName;
    public int totalStars, levelIndex;
    public bool isUnlockedByDefault;
}
