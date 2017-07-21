using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollision : MonoBehaviour {
    private float nextActionTime = 0.0f;
    public float period = 6.0f;
    public float health = 100.0f;
    public float tickDMG = 1.0f;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            health = loseHP(tickDMG);
            // execute block of code here
        }
    }

    float loseHP(float amount)
    {
        return health -= amount;
    }

    float gainHP(float amount)
    {
        return health -= amount;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
            Debug.Log("Hi");
        }
    }

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
			Debug.Log("Hi");
	}
		
}
