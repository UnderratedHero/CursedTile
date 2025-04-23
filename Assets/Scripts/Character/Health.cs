using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _healthMax = 15f;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private AudioSource _deathSource;
    [SerializeField] private AudioSource _damageSource;
    public event Action OnEntityDead;
    private float _health;
    private float _currentMaxHealth;

    public float EntityHealth { get { return _health; } }
    public float EntityMaxHealth { get { return _healthMax; } }

    private void Awake()
    {
        _health = _healthMax;
    }

    public void Damage(float damage)
    {
        _health -= damage;
        _particles.Play();
        _damageSource.Play();
        if (_health <= 0)
        {
            _deathSource.Play();
            Death();
        }
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

    public void BuffHealth(float health)
    {
        _currentMaxHealth = _healthMax;
        _healthMax += health;
        Heal(_healthMax);
    }

    public void ResetHealth()
    {
        _healthMax = _currentMaxHealth;
    }
}
