using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assets.Player.Scripts;

public class PlayerTest {

    private GameObject _terrain;
    private GameObject _player;

    [SetUp]
    public void Setup()
    {
        _terrain = Object.Instantiate(Resources.Load<GameObject>("Land"));
        _player = Object.Instantiate(Resources.Load<GameObject>("Player"));
    }

    [UnityTest]
    public IEnumerator CollisonWithCraterTest()
    {
        var crater = Object.Instantiate(Resources.Load<GameObject>("Crater"));

        yield return new WaitUntil(() => Physics.CheckBox(_player.transform.position, crater.transform.position));

        Assert.True(_player.GetComponent<PlayerController>().Health == 0);
    }
}
