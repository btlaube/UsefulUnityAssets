using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSceneTransitionManager : MonoBehaviour
{
    private SceneTransitionManager transitionManager;
    private AdditiveSceneManager additiveSceneManager;

    void Start()
    {
        transitionManager = SceneTransitionManager.Instance;
        additiveSceneManager = AdditiveSceneManager.Instance;
    }

    public void LoadSceneAdditively(Transition transition)
    {
        additiveSceneManager.LoadSceneAdditively(transition);
    }

    public void UnloadScene(string sceneName)
    {
        additiveSceneManager.UnloadScene(sceneName);
    }

    public void LoadScene(int sceneToLoad)
    {
        LoadScene(sceneToLoad, 1f, 0);
    }

    public void LoadScene(int sceneToLoad = -1, float duration = 1f, int transitionIndex = 0)
    {
        transitionManager.LoadScene(sceneToLoad, duration, transitionIndex);
    }

    public void StartTransition(int transitionIndex = 0)
    {
        transitionManager.StartTransition(transitionIndex);
    }

    public void EndTransition(int transitionIndex = 0)
    {
        transitionManager.EndTransition(transitionIndex);
    }
}
