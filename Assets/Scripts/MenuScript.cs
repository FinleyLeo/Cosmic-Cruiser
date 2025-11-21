using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, levelSelectScreen;

    public void PlayButton()
    {
        TransitionManager.instance.SwitchScene("Prototyping");
    }

    public void LevelScreenButton()
    {
        TransitionManager.instance.SwitchScene("Level Select");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {

    }
}
