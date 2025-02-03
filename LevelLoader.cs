using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadScene(int sceneIndex = -1)
    {
        if (sceneIndex == -1)
        {
            StartCoroutine(LoadNextScene());
        }
        else
        {
            StartCoroutine(LoadSpecificScene(sceneIndex));
        }
    }

    IEnumerator LoadNextScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    IEnumerator LoadSpecificScene(int sceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
    public void StartTransitionOut()
    {
        StartCoroutine(TransitionOut());
    }

    IEnumerator TransitionOut()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }
}
