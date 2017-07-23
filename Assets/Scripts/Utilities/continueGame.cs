using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class continueGame : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Mouse1") || Input.touchCount > 0)
		{
			SceneManager.LoadScene("test",LoadSceneMode.Single);
		}
	}
}
