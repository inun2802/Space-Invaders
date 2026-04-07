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
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(nextSceneIndex);
    }

    IEnumerator LoadSpecificScene(int sceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
