using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ProjectileTest
{
    private GameObject _projectile;
    private GameObject _object;

    [OneTimeSetUp]
    public void FixSetup()
    {
        Object.Instantiate(Resources.Load<GameObject>("Land"));
    }

    [SetUp]
    public void Setup()
    {
        _projectile = Object.Instantiate(Resources.Load<GameObject>("Projectile"));
    }
    
    [UnityTest]
    public IEnumerator CollisonWithEnemyTest()
    {
        _object = Object.Instantiate(Resources.Load<GameObject>("Enemy"));
        
        // Destroy() object become null on next frame
        yield return new WaitForSeconds(2f);

        Assert.IsTrue(_object == null, "Enemy should disappear!");
        Assert.IsTrue(_projectile == null, "Projectile should disappear!");
    }

    [UnityTest]
    public IEnumerator CollisonWithObstacleTest()
    {
        _object = Object.Instantiate(Resources.Load<GameObject>("Obstacle"));

        // Destroy() object become null on next frame
        yield return new WaitForSeconds(2f);

        Assert.IsFalse(_object == null, "Obstacle should not disapper!");
        Assert.IsTrue(_projectile == null, "Projectile should disappear!");
    }
}
