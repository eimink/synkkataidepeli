using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainHP : MonoBehaviour {

    public float HPGranted;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("HP granted");
            col.gameObject.SendMessage("gainHP", HPGranted);
        }
    }
}
