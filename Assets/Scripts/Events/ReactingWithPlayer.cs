using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactingWithPlayer : MonoBehaviour {
    public string keyName;

    private GameObject player;
    private Inventory checkItem;
    // Use this for initialization
    void Awake () {
		GameObject.Find("GameController").GetComponent<GameController>().onGameInitialized += delegate(object sender, System.EventArgs e)
		{
			Init();
		};
    }

	void Init () {
		player = GameObject.FindWithTag("Player");
		checkItem = (Inventory)player.GetComponent(typeof(Inventory));
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (checkItem.getInventory(keyName)) {
                SetAllCollidersStatus(false);
            }
        }
    }
    private void SetAllCollidersStatus(bool active)
    {
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = active;
        }
    }
}
