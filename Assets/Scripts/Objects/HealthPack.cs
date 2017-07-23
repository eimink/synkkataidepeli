using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPack : MonoBehaviour {

	public void set(bool coffee) {
		if (!coffee)
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/healt_cheese");
		else
			this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/health_coffee");
	}

}
