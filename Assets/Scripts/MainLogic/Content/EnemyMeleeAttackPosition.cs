using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackPosition : MonoBehaviour
{
    [SerializeField] private List<string> _attackLayerName;
    [SerializeField] private MeleeAttack _melee;
    [SerializeField] private GameObject _checkZone;
    [SerializeField] private float _attackTimeDelay = 3f;
    [SerializeField] private float _attackActiveTime = 1f;
    [SerializeField] private float _damage = 3f;

    private bool _isAttacking = false;
    private float _timer = 0f;
    private float _activeTimer = 0f;

    private void Start()
    {
        _melee.SetDamage(_damage);
    }

    private void Update()
    {
        ReadAttack();
    }

    private void ReadAttack()
    {
        if (!_isAttacking)
            return;

        _activeTimer += Time.deltaTime;

        if (_activeTimer > _attackActiveTime)
        {
            _activeTimer = 0f;
            _melee.gameObject.SetActive(false);
        }

        _timer += Time.deltaTime;
        if (_timer > _attackTimeDelay)
        {
            _isAttacking = false;
            _timer = 0f;
            _checkZone.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_checkZone.activeInHierarchy || _isAttacking)
            return;

        foreach (string attackLayer in _attackLayerName)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer(attackLayer))
                continue;

            _isAttacking = true;
            _melee.transform.position = collision.transform.position;

            var direction = collision.transform.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _melee.transform.rotation = Quaternion.Euler(0, 0, angle);

            _melee.gameObject.SetActive(true);
            _checkZone.SetActive(false);
            break;
        }
    }
}
