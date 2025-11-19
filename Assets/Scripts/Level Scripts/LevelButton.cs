using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    bool isLocked = true;

    private void Start()
    {
        if (levelData.isUnlockedByDefault)
        {
            isLocked = false;
        }

        // Grey out or change sprite, dont allow it to be interacted with
        if (isLocked)
        {

        }

        // Allow interactions, sending player to corresponding level
        else
        {

        }
    }

    private void Update()
    {
        
    }
}
