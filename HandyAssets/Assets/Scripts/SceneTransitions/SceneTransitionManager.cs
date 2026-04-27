using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    public Animator[] transitions;
    
    [SerializeField] private const float transitionTime = 1f;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int sceneToLoad = -1, float duration = transitionTime, int transitionIndex = 0)
    {
        StartCoroutine(LoadLevel(sceneToLoad, duration, transitionIndex));
    }

    IEnumerator LoadLevel(int levelIndex, float duration, int transitionIndex)
    {
        transitions[transitionIndex].SetTrigger("Start");

        yield return new WaitForSeconds(duration);

        if (levelIndex != -1) SceneManager.LoadScene(levelIndex);        

        transitions[transitionIndex].SetTrigger("End");
    }

    public void LoadSceneAdditively(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void StartTransition(int transitionIndex = 0)
    {
        transitions[transitionIndex].SetTrigger("Start");
    }

    public void EndTransition(int transitionIndex = 0)
    {
        transitions[transitionIndex].SetTrigger("End");
    }

}
