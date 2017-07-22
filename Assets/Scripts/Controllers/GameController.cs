using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private bool gameOver;
    private bool restart;

    void Start ()
    {
        gameOver = false;
        restart = false;
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
}
