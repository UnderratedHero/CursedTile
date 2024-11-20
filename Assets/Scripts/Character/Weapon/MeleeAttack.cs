using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float _damage;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Health>(out var heath))
            return;
     
        heath.Damage(_damage);
    }
}
