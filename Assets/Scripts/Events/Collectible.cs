using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class integerChange : MonoBehaviour
{

    public float intChange;
    public bool staysActive = false;
    public string methodToCall;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage(methodToCall, intChange);
            gameObject.SetActive(staysActive);
        }
    }
}

