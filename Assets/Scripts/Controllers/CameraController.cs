using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Camera mainCamera;
    private GameObject player;
	public float cameraOffset = 10;

	// Use this for initialization
	void Start () {
		mainCamera = GetComponent<Camera>();
		player = (GameObject)GameObject.FindGameObjectsWithTag("Player")[0];
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerInfo = player.transform.transform.position;
		mainCamera.transform.position = new Vector3(playerInfo.x, playerInfo.y, playerInfo.z - cameraOffset);
	}
}
