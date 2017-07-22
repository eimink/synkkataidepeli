using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{

    GameObject[] storypoints;

    void Start()
    {
        SceneManager.LoadScene("storyline", LoadSceneMode.Additive);
    }

    void Update()
    {
	}

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening
        // for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
         
    void OnDisable()
    {
        // Tell our 'OnLevelFinishedLoading' function to stop listening
        // for a scene change as soon as this script is disabled.
        // Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
         
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "storyline" && mode == LoadSceneMode.Additive)
        {
            storypoints = GameObject.FindGameObjectsWithTag("StoryPoint");
            for (int i = 0; i < storypoints.Length; ++i)
            {
                Debug.Log("Setting storypoint" + i + " inactive");
                storypoints[i].SetActive(false);
            }
        }
    }

    public void ActivateStorypoint(string name)
    {
        Debug.Log("Activating storypoint " + name);
        for(int i = 0; i < storypoints.Length; ++i)
        {
            if(storypoints[i].name == name) {
                Debug.Log("Found storypoint for activation");
                storypoints[i].SetActive(true);
            }
        }
        /*Debug.Log("searching " + id);
        GameObject storypoint = storypoints[i]
        storypoint.SetActive(true);
        Input.GetKeyDown(KeyCode.Space);
        storystate += 1;*/
    }

    public void DeactivateStorypoint(string name) {
        Debug.Log("Deactivating storypoint " + name);
        for(int i = 0; i < storypoints.Length; ++i)
        {
            if(storypoints[i].name == name) {
                Debug.Log("Found storypoint for deactivation");
                storypoints[i].SetActive(false);
            }
        }
    }
}