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
        string id = "storypoint" + storystate;
        Debug.Log("searching " + id);
        canvasController.SendMessage("ActivateStorypoint", id);
        Input.GetKeyDown(KeyCode.Space);
        canvasController.SendMessage("DeactivateStorypoint", id);
        storystate += 1;
    }

}
