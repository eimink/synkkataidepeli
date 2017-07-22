using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private bool gameOver;
    private bool restart;
    private int storystate;
    private GameObject canvasController;

    void Start ()
    {
        gameOver = false;
        restart = false;
        canvasController = GameObject.Find("CanvasController");
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
