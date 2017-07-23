using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{

    private bool gameOver;
    private bool restart;
    private bool backToMenu;
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
        backToMenu = false;
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
		Vector3 spawnPos = new Vector3(spawnPoint.transform.position.x,spawnPoint.transform.position.y,-2f);
		c.player = GameObject.Instantiate(dickButt,spawnPos,Quaternion.identity);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(gameOver)
        {
            canvasController.SendMessage("ActivateStorypoint", "credits");
            restart = true;
            gameOver = false;
        }
        if(backToMenu) {
            SceneManager.LoadScene("startscene");
        }
	}

    public void PlayerDied()
    {
        string id = "restart";
        canvasController.SendMessage("ActivateStorypoint", "restart");
    }

    public void PlayerFinished()
    {
        gameOver = true;
    }

    public void BackToMenu()
    {
        backToMenu = true;
    }

    public void RestartGame()
    {
        restart = true;
    }

    public void ShowStorypoint()
    {
        string id = getStorypointID();
        Debug.Log("Showing storypoint for " + id);
        canvasController.SendMessage("ActivateStorypoint", id);
        storystate += 1;
    }

    public void ShowGameNotFinished()
    {
        string id = "gamenotfinished";
        canvasController.SendMessage("ActivateStorypoint", id);
    }

    private string getStorypointID() {
        return "storypoint" + storystate;
    }
}
