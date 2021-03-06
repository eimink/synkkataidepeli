﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class LevelGenerator : MonoBehaviour {

	public Color spawnColor;
	public Color goalColor;
	public Color interestColor;
	public Color enemyColor;
	public Color healthPackColor;
	public Color blockerColor;
	public Color blockerColor2;
	public Color keyColor;
	public GeneratorBlock[] blocks;
	public GameObject wallTile;
	public GameObject floorTile;
	public Color floorColor;
	public bool Ready {get{return m_ready;}}
	public event EventHandler onLevelGenerated;

	public Material riverCorner;

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
		Color tileColor;
		GenerateBounds(width,height);
		GenerateFloor(width,height);

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				tileColor = pixels[i*width+j];
				int idx = FindBlockIndex(tileColor); // Get block from bitmap data
				if (idx >= 0)
				{
					GameObject o = (GameObject)Instantiate(blocks[idx].prefab,new Vector3(j,i,-1),Quaternion.identity);
					o.transform.parent = m_levelParent.transform;
					if (o.name == "River(Clone)")
					{
						ChooseRiverBlock(Convert.ToInt32(tileColor.r*255),o);
					}
					if (o.name == "Hazard(Clone)")
					{
						int rnd = UnityEngine.Random.Range(0,2);
						if (rnd == 0)
							o.transform.Find("HazardKnife").gameObject.SetActive(false);
						else
							o.transform.Find("HazardNoose").gameObject.SetActive(false);
					}
					if (tileColor == spawnColor)
					{
						o.tag = "SpawnPoint";
					}
					else if (tileColor == goalColor)
					{
						o.tag = "Goal";
					}
					else if (tileColor == interestColor)
					{
						o.tag = "StoryPoint";
					}
					else if (tileColor == healthPackColor)
					{
						o.tag = "HealthPack";
						o.GetComponent<HealthPack>().set(UnityEngine.Random.value > .5f);
					}
					else if (tileColor == enemyColor)
					{
						o.tag = "Enemy";
					}
					else if (tileColor.b == blockerColor.b && tileColor.g == blockerColor.g)
					{
						o.tag = "Blocker";
						o.GetComponent<ReactingWithPlayer>().keyName = "key"+Convert.ToInt32(tileColor.r*255);
					}
					else if (tileColor.b == blockerColor2.b && tileColor.g == blockerColor2.g)
					{
						o.tag = "Blocker";
						o.GetComponent<ReactingWithPlayer>().keyName = "key"+Convert.ToInt32(tileColor.r*255);
					}
					else if (tileColor.b == keyColor.b && tileColor.g == keyColor.g)
					{
						o.tag = "Key";
						int id = Convert.ToInt32(tileColor.r*255);
						o.GetComponent<Key>().set(id);
						if (o.GetComponent<pickupInventoryItem>() != null)
							o.GetComponent<pickupInventoryItem>().keyName = "key"+id;
					}
				}
			}
		}
		m_ready = true;
		if (onLevelGenerated != null)
		{
			Debug.Log("onLevelGenerated");
			onLevelGenerated(this,null);
		}
	}

	protected void ChooseRiverBlock(int i, GameObject o)
	{
		switch (i) {
			case 1:
				o.transform.Rotate(Vector3.forward * 90);
				break;
			case 2:
				o.GetComponent<MeshRenderer>().material = riverCorner;
				o.transform.Rotate(Vector3.forward * 180);
				break;
			case 3:
				o.GetComponent<MeshRenderer>().material = riverCorner;
				o.transform.Rotate(Vector3.forward * -270);
				break;
			case 4:
				o.GetComponent<MeshRenderer>().material = riverCorner;
				break;
			case 5:
				o.GetComponent<MeshRenderer>().material = riverCorner;
				o.transform.Rotate(Vector3.forward * -90);
				break;
			default:
				break;
		}
	}

	protected void GenerateBounds(int width, int height)
	{
		GameObject o = (GameObject)Instantiate(wallTile,new Vector3(width/2,0,0),Quaternion.identity);
		o.transform.localScale += new Vector3(width,0,0);
		o.transform.parent = m_levelParent.transform;
		o = (GameObject)Instantiate(wallTile,new Vector3(width/2,height,0),Quaternion.identity);
		o.transform.localScale += new Vector3(width,0,0);
		o.transform.parent = m_levelParent.transform;
		o = (GameObject)Instantiate(wallTile,new Vector3(0,height/2,0),Quaternion.identity);
		o.transform.localScale += new Vector3(0,height,0);
		o.transform.parent = m_levelParent.transform;
		o = (GameObject)Instantiate(wallTile,new Vector3(width,height/2,0),Quaternion.identity);
		o.transform.localScale += new Vector3(0,height,0);
		o.transform.parent = m_levelParent.transform;
	}

	protected void GenerateFloor(int width, int height)
	{
		GameObject o = (GameObject)Instantiate(floorTile,new Vector3(width/2,height/2,0),Quaternion.identity);
		o.transform.localScale += new Vector3(width,height,0);
		o.transform.parent = m_levelParent.transform;
	}

	protected int FindBlockIndex(Color c)
	{
		for (int i = 0; i < blocks.Length; i++) 
		{
			if (blocks[i].key == c)
			{
				return i;
			}
		}
		return FindBlockIndexBG(c);
	}

	protected int FindBlockIndexBG(Color c)
	{
		int red = Convert.ToInt32(c.r * 255);
		int blue = Convert.ToInt32(c.b * 255);
		int green = Convert.ToInt32(c.g * 255);
		if (red >= 1 && red <= 7)
		{
			for (int i = 0; i < blocks.Length; i++) 
			{
				int b2 = Convert.ToInt32(blocks[i].key.b*255);
				int g2 = Convert.ToInt32(blocks[i].key.g*255);
				if (b2 == blue && g2 == green)
				{
					return i;
				}
			}
		}
		return -1;
	}
}
