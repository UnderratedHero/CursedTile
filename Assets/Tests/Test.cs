using NUnit.Framework;
using UnityEngine;

public class Test
{
    private GameObject _healthGameObject;
    private HealthTest _health;

    [SetUp]
    public void Setup()
    {
        _healthGameObject = new GameObject();
        _health = _healthGameObject.AddComponent<HealthTest>();
        _health.Awake();
    }

    [Test]
    public void TestInitialHealth_IsSetToMaxHealth()
    {
        Assert.AreEqual(_health.EntityMaxHealth, _health.EntityHealth);
    }

    [Test]
    public void TestDamage_ReducesHealthCorrectly()
    {
        var initialHealth = _health.EntityHealth;
        float damageAmount = 5f;

        _health.Damage(damageAmount);

        Assert.AreEqual(initialHealth - damageAmount, _health.EntityHealth);
    }

    [Test]
    public void TestDamage_TriggersDeath_WhenHealthDropsToZero()
    {
        bool deathInvoked = false;
        _health.OnEntityDead += () => deathInvoked = true;

        _health.Damage(_health.EntityMaxHealth);

        Assert.IsTrue(deathInvoked);
        Assert.IsTrue(_healthGameObject == null || !_healthGameObject);
    }

    [Test]
    public void TestHeal_IncreasesHealthCorrectly()
    {
        _health.Damage(10f);
        var reducedHealth = _health.EntityHealth;
        float healAmount = 5f;

        _health.Heal(healAmount);

        Assert.AreEqual(reducedHealth + healAmount, _health.EntityHealth);
    }

    [Test]
    public void TestHeal_DoesNotExceedMaxHealth()
    {
        _health.Damage(5f);
        _health.Heal(10f);

        Assert.AreEqual(_health.EntityMaxHealth, _health.EntityHealth);
    }

    [Test]
    public void TestDeath_DestroysGameObjectAndInvokesEvent()
    {
        bool deathEventTriggered = false;
        _health.OnEntityDead += () => deathEventTriggered = true;

        _health.Damage(_health.EntityMaxHealth);

        Assert.IsTrue(deathEventTriggered);
        Assert.IsTrue(_healthGameObject == null || !_healthGameObject);
    }

    [TearDown]
    public void Teardown()
    {
        if (_healthGameObject != null)
        {
            UnityEngine.Object.DestroyImmediate(_healthGameObject);
        }
    }
}

