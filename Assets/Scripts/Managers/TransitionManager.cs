using System.Collections;
using Unity.VisualScripting;
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

    private void OnEnable()
    {
        GameEvents.OnLevelReset += RestartLevel;
        GameEvents.OnGameOver += GameOverRestart;
    }

    private void OnDisable()
    {
        GameEvents.OnLevelReset -= RestartLevel;
        GameEvents.OnGameOver -= GameOverRestart;
    }

    #region Switch Scene
    public void SwitchScene(int buildIndex, float delay)
    {
        StartCoroutine(LoadLevel(buildIndex, delay));
    }

    public void SwitchScene(int buildIndex)
    {
        StartCoroutine(LoadLevel(buildIndex));
    }

    public void SwitchScene(string buildName, float delay)
    {
        StartCoroutine(LoadLevel(buildName, delay));
    }

    public void SwitchScene(string buildName)
    {
        StartCoroutine(LoadLevel(buildName));
    }

    IEnumerator LoadLevel(int buildIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        transAnim.Play("FadeOut");

        yield return new WaitForSeconds(transAnim.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(buildIndex);

        yield return new WaitForSeconds(0.2f);

        transAnim.Play("FadeIn");
    }

    IEnumerator LoadLevel(int buildIndex)
    {
        transAnim.Play("FadeOut");

        yield return new WaitForSeconds(transAnim.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(buildIndex);

        yield return new WaitForSeconds(0.2f);

        transAnim.Play("FadeIn");
    }

    IEnumerator LoadLevel(string buildName, float delay)
    {
        yield return new WaitForSeconds(delay);

        transAnim.Play("FadeOut");

        yield return new WaitForSeconds(transAnim.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(buildName);

        yield return new WaitForSeconds(0.2f);

        transAnim.Play("FadeIn");
    }

    IEnumerator LoadLevel(string buildName)
    {
        transAnim.Play("FadeOut");

        yield return new WaitForSeconds(transAnim.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(buildName);

        yield return new WaitForSeconds(0.2f);

        transAnim.Play("FadeIn");
    }
    #endregion

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void GameOverRestart()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex, 0.5f));
    }
}
