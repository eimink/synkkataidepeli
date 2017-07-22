using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingChange : MonoBehaviour {

    public float floatChange;
    public bool staysActive=false;
    public string methodToCall;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage(methodToCall, floatChange);
            gameObject.SetActive(staysActive);
        }
    }
}
