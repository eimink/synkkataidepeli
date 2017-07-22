using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupInventoryItem : MonoBehaviour {


    public string keyName;
    public bool staysActive = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("setInventory", keyName);
            gameObject.SetActive(staysActive);
        }
    }
}
