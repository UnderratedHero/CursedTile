using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private List<string> _attackLayerName;
    [SerializeField] private float _deleteTime = 3f;
    private float _timer = 0f;
    private float _damage;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _deleteTime)
        {
            _timer = 0f;
            Destroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string attackLayer in _attackLayerName)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer(attackLayer))
                continue;


            if (collision.TryGetComponent<Health>(out var heath))
                heath.Damage(_damage);

            Destroy();
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
