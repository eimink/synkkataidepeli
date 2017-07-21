﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

	public Color spawnColor;
	public GeneratorBlock[] blocks;
	public GameObject wallTile;
	public GameObject floorTile;
	public Color floorColor;
	public bool Ready {get{return m_ready;}}

	GameObject m_levelParent;
	bool m_ready = false;

	// Use this for initialization
	void Start () {
		Init();
	}

	protected void Init() {
		m_ready = false;
		if (m_levelParent != null)
			Destroy (m_levelParent);
		if (GameObject.Find("GeneratedLevel") != null)
			Destroy (GameObject.Find("GeneratedLevel"));
		m_levelParent = new GameObject ();
		m_levelParent.name = "GeneratedLevel";
	}

	protected void GenerateLevelFromBitmap(Texture2D bitmap)
	{
		Color[] pixels = bitmap.GetPixels();
		int width = bitmap.width;
		int height = bitmap.height;
		GameObject tile;
		GenerateBounds(width,height);
		GenerateFloor(width,height);
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				int idx = FindBlockIndex(pixels[i*width+j]); // Get block from bitmap data
				if (idx >= 0)
				{
					GameObject o = (GameObject)Instantiate(blocks[idx].prefab,new Vector3(i,0,j),Quaternion.identity);
					o.transform.parent = m_levelParent.transform;
					if (pixels[i*width+j] == spawnColor)
					{
						o.tag = "SpawnPoint";
					}
				}
			}
		}
		m_ready = true;
	}

	protected void GenerateBounds(int width, int height)
	{
		GameObject o = (GameObject)Instantiate(wallTile,new Vector3(width/2,0,0),Quaternion.identity);
		o.transform.localScale += new Vector3(width,0,0);
		o.transform.parent = m_levelParent.transform;
		o = (GameObject)Instantiate(wallTile,new Vector3(width/2,0,height),Quaternion.identity);
		o.transform.localScale += new Vector3(width,0,0);
		o.transform.parent = m_levelParent.transform;
		o = (GameObject)Instantiate(wallTile,new Vector3(0,0,height/2),Quaternion.identity);
		o.transform.localScale += new Vector3(0,0,height);
		o.transform.parent = m_levelParent.transform;
		o = (GameObject)Instantiate(wallTile,new Vector3(width,0,height/2),Quaternion.identity);
		o.transform.localScale += new Vector3(0,0,height);
		o.transform.parent = m_levelParent.transform;
	}

	protected void GenerateFloor(int width, int height)
	{
		GameObject o = (GameObject)Instantiate(floorTile,new Vector3(width/2,0,height/2),Quaternion.identity);
		o.transform.localScale += new Vector3(width,0,height);
		o.transform.parent = m_levelParent.transform;
	}

	protected int FindBlockIndex(Color c)
	{
		for (int i = 0; i < blocks.Length; i++) 
		{
			if (blocks [i].key == c)
			{
				return i;
			}
		}
		return -1;
	}
}
