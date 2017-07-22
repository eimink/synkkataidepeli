using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Camera mainCamera;
    public GameObject player;
	public float cameraOffset = 10;

	// Use this for initialization
	void Start () {
		mainCamera = GetComponent<Camera>();
		//player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null)
		{
			Vector3 playerInfo = player.transform.transform.position;
			mainCamera.transform.position = new Vector3(playerInfo.x, playerInfo.y, playerInfo.z - cameraOffset);
		}
	}
}
