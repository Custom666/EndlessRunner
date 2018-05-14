using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assets.Player.Scripts;

public class PlayerTest
{
    private PlayerController _player;
    private GameObject _object;

    [OneTimeSetUp]
    public void FixSetup()
    {
        Object.Instantiate(Resources.Load<GameObject>("Land"));
    }

    [SetUp]
    public void Setup()
    {
        _player = Object.Instantiate(Resources.Load<GameObject>("Player")).GetComponent<PlayerController>();
    }

    [TearDown]
    public void Clear()
    {
        Object.Destroy(_player.gameObject);
        Object.Destroy(_object);
    }
    
    [UnityTest]
    public IEnumerator CollisonWithCraterTest()
    {
        _object = Object.Instantiate(Resources.Load<GameObject>("Crater"));

        yield return new WaitUntil(() => Physics.CheckBox(_player.transform.position, _object.transform.position));

        yield return new WaitForSeconds(.1f);

        Assert.AreEqual(0, _player.Health, "Player healt should be zero => player should be death");
    }

    [UnityTest]
    public IEnumerator CollisonWithEnemyTest()
    {
        _object = Object.Instantiate(Resources.Load<GameObject>("Enemy"));

        var healtBefore = _player.Health;

        yield return new WaitUntil(() => Physics.CheckBox(_player.transform.position, _object.transform.position));

        Assert.AreEqual(_player.Health, healtBefore - 1, "Player healt should be decrement!");
    }

    [UnityTest]
    public IEnumerator CollisonWithObstacleTest()
    {
        _object = Object.Instantiate(Resources.Load<GameObject>("Obstacle"));

        var healtBefore = _player.Health;

        yield return new WaitUntil(() => Physics.CheckBox(_player.transform.position, _object.transform.position));

        Assert.AreEqual(_player.Health, healtBefore - 1, "Player healt should be decrement!");
    }
}
