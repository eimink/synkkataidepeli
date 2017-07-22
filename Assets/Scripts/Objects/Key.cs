using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Key : MonoBehaviour {

	enum keys {
		collectable_Key_blue = 1,
		collectable_Key_green,
		collectable_Key_pinkish,
		collectable_Key_purple,
		collectable_Key_red,
		collectable_Key_turquoise
	}

	public void set(int i) {
		Debug.Log(i);
		keys key = (keys)i;
		this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/"+Enum.GetName(key.GetType(),key));
	}

}
