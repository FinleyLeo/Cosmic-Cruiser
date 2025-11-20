using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    bool isActive = false;
    Vector2 screenPosition;

    void OnEnable()
    {
        GameEvents.OnLevelStart += LevelStarted;
    }

    void OnDisable()
    {
        GameEvents.OnLevelStart -= LevelStarted;
    }

    private void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (isActive)
        {
            CheckBounds();
        }
        
    }

    void CheckBounds()
    {
        if (screenPosition.x > 1.05f || screenPosition.x < -0.05f || screenPosition.y > 1.05f || screenPosition.y < -0.05f)
        {
            if (gameObject.CompareTag("Player"))
            {
                isActive = false;
                GameEvents.InvokeLevelFailed();
            }

            else
            {
                Destroy(gameObject);
            }
        }
    }

    void LevelStarted()
    {
        isActive = true;
    }
}
