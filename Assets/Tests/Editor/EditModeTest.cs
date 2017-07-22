using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EditModeTest {

	[Test]
	public void EditModeTestSimplePasses() {
        // Use the Assert class to test conditions.
        Assert.AreEqual(true, true);
    }
    [Test]
    public void GameObject_CreatedWithGiven_WillHaveTheName()
    {
        var go = new GameObject("dickbutt");
        Assert.AreEqual("dickbutt", go.name);
    }
    [Test]
    public void IsDickbuttActive()
    {
        var go = new GameObject("dickbutt");
        Assert.AreEqual(true, go.active);
    }
    [Test]
    public void CollectiblesWork()
    {
        var go = new InventoryUtils();
        go.collectItem(1);
        Assert.AreEqual(1, go.collectedItems());
    }
    [Test]
    public void InventoryWorkTest()
    {
        var go = new InventoryUtils();
        go.setInventoryData("VerySeriousKey");
        Assert.AreEqual(true, go.getInventoryData().Contains("VerySeriousKey"));
    }
    [Test]
    public void InventoryShouldntContainData()
    {
        var go = new InventoryUtils();
        go.setInventoryData("VerySeriousKey");
        Assert.AreEqual(false, go.getInventoryData().Contains("dickbutt"));
    }
    [Test]
    public void InventoryShouldntContainDataNoData()
    {
        var go = new InventoryUtils();
        Assert.AreEqual(false, go.getInventoryData().Contains("dickbutt"));
    }
    [Test]
    public void PlayerHP()
    {
        var go = new PlayerUtils();
        
        Assert.AreEqual(100, go.getHealth());
    }
    [Test]
    public void PlayerHPGainDoesntGoOverHundred()
    {
        var go = new PlayerUtils();
        go.HPGrant(50);
        Assert.AreEqual(100, go.getHealth());
    }
    [Test]
    public void PlayerHPLose()
    {
        var go = new PlayerUtils();
        go.HPRemove(50);
        Assert.AreEqual(50, go.getHealth());
    }

    public void PlayerHPGain()
    {
        var go = new PlayerUtils();
        go.HPRemove(50);
        go.HPGrant(25);
        Assert.AreEqual(75, go.getHealth());
    }
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
	public IEnumerator EditModeTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        yield return null;
	}
}
