using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private List<string> _attackLayerName;
    private float _damage;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string attackLayer in _attackLayerName)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer(attackLayer) ||
                !collision.TryGetComponent<Health>(out var heath))
                continue;

            heath.Damage(_damage);
        }
    }
}
