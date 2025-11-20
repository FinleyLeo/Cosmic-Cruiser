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
    }

    private void OnDisable()
    {
        GameEvents.OnLevelReset -= RestartLevel;
    }

    public void SwitchScene(int buildIndex, float delay)
    {
        StartCoroutine(LoadLevel(buildIndex, delay));
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

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex, 0.5f));
    }
}
