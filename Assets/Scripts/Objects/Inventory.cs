using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;


public class Inventory : MonoBehaviour {
    public int collectablesMax;
    public Text collectablesText;
    public Image[] keyImages = new Image[5];
    private InventoryUtils invUtils = new InventoryUtils();

    void Start() {
        collectablesMax = 7;
        setCollectables();
        disableKeyImages();
    }

    public void setInventory(string item) {
        invUtils.setInventoryData(item);
        Debug.Log(item + " added to the inventory");
        enableKeyImage(item);
    }

    public bool getInventory(string item)
    {
        bool inventoryItem = invUtils.getInventoryData().Contains(item);
        return inventoryItem;
    }

    private void enableKeyImage(string item)
    {
        keyImages.Where(x => x.name.Contains(item)).First().enabled = true;
    }

    private void disableKeyImages()
    {
        for (int i = 0; i < keyImages.Count(); i++)
        {
            keyImages[i].enabled = false;
        }
    }

    private void setCollectables()
    {
        collectablesText.text = "Heart Pieces: " + invUtils.collectedItems().ToString() + "/" + collectablesMax.ToString();
    }

    public void collectedItem(int collected)
    {
        invUtils.collectItem(collected);
        setCollectables();
        GameObject player = this.gameObject;
        player.SendMessage("sendGameEvent", "ShowStorypoint");
    }
}

