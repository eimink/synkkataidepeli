using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{

    Dictionary<string, GameObject> storypoints;
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
            if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) {
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
            storypoints = new Dictionary<string, GameObject>();
            GameObject[] objs = GameObject.FindGameObjectsWithTag("StoryPoint");
            for (int i = 0; i < objs.Length; ++i)
            {
                GameObject o = objs[i];
                Debug.Log("Setting storypoint" + o.name + " inactive");
                o.SetActive(false);
                storypoints.Add(o.name, o);
            }
        }
    }

    public void ActivateStorypoint(string name)
    {
        Debug.Log("Activating storypoint " + name);
        storypoints[name].SetActive(true);
        activeCanvasID = name;
    }

    public void DeactivateStorypoint() {
        storypoints[activeCanvasID].SetActive(false);
        activeCanvasID = "";
    }
}