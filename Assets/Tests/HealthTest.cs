using System;
using UnityEngine;

public class HealthTest : MonoBehaviour
{
    [SerializeField] private float _healthMax = 15f;
    public event Action OnEntityDead;
    private float _health;

    public float EntityHealth { get { return _health; } }
    public float EntityMaxHealth { get { return _healthMax; } }

    public void Awake()
    {
        _health = _healthMax;
    }

    public void Damage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
            Death();
    }

    public void Heal(float heal)
    {
        _health += heal;
        if (_health > _healthMax)
            _health = _healthMax;
    }

    public void Death()
    {
        OnEntityDead?.Invoke();
        Destroy(gameObject);
    }
}
