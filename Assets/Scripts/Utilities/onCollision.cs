using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public float health = 100;

    float loseHP(float amount)
    {
        return health = health - amount;
    }

    float gainHP(float amount)
    {
        return health = health + amount;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Cube(Clone)" )
        {
            Debug.Log("Hi");
        }
    }
}
