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
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
	public IEnumerator EditModeTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        yield return null;
	}
}
