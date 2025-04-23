using System.Collections.Generic;
using UnityEngine;

public class TrapRangeAttackPosition : MonoBehaviour
{
    [SerializeField] private List<string> _attackLayerName;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _damage; 
    [SerializeField] private float _force; 
    [SerializeField] private CheckZone _checkZone;
    [SerializeField] private ExitZone _exitZone;
    [SerializeField] private float _attackTimeDelay = 1f;

    private Transform _currentTarget;
    private float _attackTimer = 0f; 
    private bool _canAttack = false;

    private void OnEnable()
    {
        _checkZone.OnEnter += OnEnter;
        _exitZone.OnExit += OnExit;
    }

    private void OnDisable()
    {
        _checkZone.OnEnter -= OnEnter;
        _exitZone.OnExit -= OnExit;
    }

    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {

        if (_currentTarget == null || !_canAttack)
            return;

        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _attackTimeDelay)
        {
            _attackTimer = 0f;
            ShootAtTarget(); 
        }
    }

    private void ShootAtTarget()
    {
        var direction = (_currentTarget.position - transform.position).normalized;

        var bulletInstance = Instantiate(_prefab, transform.position, Quaternion.identity);

        var bullet = bulletInstance.GetComponent<Bullet>();
        bullet.SetDamage(_damage);

        var rigidBody = bulletInstance.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(direction * _force, ForceMode2D.Impulse);
    }


    private void OnEnter(Collider2D collision)
    {
        foreach (var attackLayer in _attackLayerName)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(attackLayer))
            {
                if (!_checkZone.enabled ||
                    _currentTarget != null)
                    return;

                _currentTarget = collision.transform;
                _canAttack = true;
                _checkZone.enabled = false;
                _exitZone.enabled = true;
                break;
            }
        }
    }

    private void OnExit(Collider2D collision)
    {
        if (_checkZone.enabled ||
       _currentTarget.position != collision.transform.position)
            return;

        _currentTarget = null;
        _canAttack = false;
        _checkZone.enabled = true;
        _exitZone.enabled = false;

    }
}
