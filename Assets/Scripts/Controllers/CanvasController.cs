using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{

    GameObject[] storypoints;
    string activeCanvasID;
    private GameObject player;
    void Awake()
    {
        activeCanvasID = "";
		SceneManager.LoadScene("storyline", LoadSceneMode.Additive);
		GameObject.Find("GameController").GetComponent<GameController>().onGameInitialized += delegate(object sender, System.EventArgs e)
			{
				Init();
			};
    }

	void Init()
	{
		
		player = GameObject.FindWithTag("Player");
	}

    void Update()
    {
        if(activeCanvasID != "") {
            if(Input.GetKeyDown(KeyCode.Space)) {
                player.SendMessage("setPlayerMovement", true);
                DeactivateStorypoint();
            }
        }
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
                activeCanvasID = name;
            }
        }
    }

    public void DeactivateStorypoint() {
        for(int i = 0; i < storypoints.Length; ++i)
        {
            if(storypoints[i].name == activeCanvasID) {
                Debug.Log("Found storypoint for deactivation");
                storypoints[i].SetActive(false);
                activeCanvasID = "";
            }
        }
    }
}