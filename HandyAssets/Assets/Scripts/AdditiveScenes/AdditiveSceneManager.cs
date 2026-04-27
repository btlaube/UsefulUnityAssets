using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{

    public static AdditiveSceneManager Instance;

    [SerializeField] private List<string> loadedScenes;
    
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

    void Start()
    {
        loadedScenes = new List<string>();
        LoadSceneAdditively("MainMenu");
    }

    public void LoadSceneAdditively(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadedScenes.Add(sceneName);
    }

    public void LoadSceneAdditively(Transition transition)
    {
        StartCoroutine(LoadLevelAdditively(transition));
    }

    IEnumerator LoadLevelAdditively(Transition transition)
    {
        GameObject transitionObject = Instantiate(transition.transitionObject, transform.position, Quaternion.identity);
        transitionObject.GetComponent<Animator>().SetTrigger("Start");

        yield return new WaitForSeconds(transition.transitionDuration);
        if (transition.scenesToLoad != null)
        {
            foreach (string scene in transition.scenesToLoad)
            {
                LoadSceneAdditively(scene);
            }
        }
        if (transition.scenesToUnload != null)
        {
            foreach (string scene in transition.scenesToUnload)
            {
                UnloadScene(scene);
            }
        }
        transitionObject.GetComponent<Animator>().SetTrigger("End");
        // loadedScenes.Add(transition.sceneToLoad);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        loadedScenes.Remove(sceneName);
    }

    public void UnloadAllScenes()
    {
        foreach (string scene in loadedScenes)
        {
            UnloadScene(scene);
        }
    }

}