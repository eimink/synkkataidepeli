using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{

    private bool gameOver;
    private bool restart;
    private int storystate;
    private GameObject canvasController;
	private LevelGenerator levelGen;
	private GameObject mainCamera;
	public GameObject dickButt;

	public event EventHandler onGameInitialized;

    void Awake ()
    {
		levelGen = GameObject.Find("LevelGenerator").GetComponent<BitmapLevelGenerator>();
        gameOver = false;
        restart = false;
        storystate = 0;
        
		levelGen.onLevelGenerated += delegate(object sender, EventArgs e)
		{
			Init();
		};
    }

	void Init()
	{
		storystate = 1;
		GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
		canvasController = GameObject.Find("CanvasController");
		mainCamera = GameObject.FindWithTag("MainCamera");
		CameraController c = mainCamera.GetComponent<CameraController>();
		if (spawnPoint == null) Debug.Log("spawn missing!");
		c.player = GameObject.Instantiate(dickButt,spawnPoint.transform.position,Quaternion.identity);
		if (onGameInitialized != null)
		{
			Debug.Log("onGameGenerated");
			onGameInitialized(this, null);
		}
	}

    void Update()
    {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
	}

    public void PlayerDied()
    {
        gameOver = true;
		restart = true;
    }

    public void ShowStorypoint()
    {
        string id = getStorypointID();
        Debug.Log("Showing storypoint for " + id);
        canvasController.SendMessage("ActivateStorypoint", id);
        storystate += 1;
    }

    private string getStorypointID() {
        return "storypoint" + storystate;
    }
}
