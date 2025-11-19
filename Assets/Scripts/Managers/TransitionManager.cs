using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    public Animator transAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SwitchScene(int buildIndex, float delay)
    {
        StartCoroutine(LoadLevel(buildIndex, delay));
    }

    IEnumerator LoadLevel(int buildIndex, float delay)
    {
        transAnim.Play("FadeIn");

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(buildIndex);
        transAnim.Play("FadeOut");
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex, 0.5f));
    }
}
