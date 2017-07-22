using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public int collectablesMax;
    public Text collectablesText;
    private int collectablesCount;

    void Start () {
        collectablesCount = 0;
        collectablesMax = 6;
        setCollectables();
    }

    private void setCollectables()
    {
        collectablesText.text = "Collectables: " + collectablesCount.ToString() + "/" + collectablesMax.ToString();
    }

    public void collectedItem(int collected)
    {
        collectablesCount += collected;
        setCollectables();
    }
}
