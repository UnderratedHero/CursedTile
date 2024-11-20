using UnityEngine;

public class Health : MonoBehaviour
{
    private const float HealthMax = 15f;
    private float _health = 15f;

    public void Damage(float damage)
    {
        Debug.Log("Get damage" + damage);
        _health -= damage;
        if (_health <= 0)
        {
            Debug.Log("Im dead");
            Death();
        }
    }

    public void Heal(float heal)
    {
        _health += heal;
        if (_health > HealthMax)
            _health = HealthMax;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
