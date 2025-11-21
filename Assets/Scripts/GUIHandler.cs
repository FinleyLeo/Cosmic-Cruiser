using UnityEngine;

public class GUIHandler : MonoBehaviour
{
    public void RestartButton()
    {
        GameEvents.InvokeLevelRestarted();
    }

    public void PauseButton()
    {
        // Temp
    }
}
