using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private CheckZone _checkZone;
    [SerializeField] private Health _health;
    [SerializeField] private List<string> _enemyLayerName;

    private Transform _target;
    private NavMeshAgent _agent;

    private void OnEnable()
    {
        _checkZone.OnEnter += CheckEnterZone;
        _checkZone.OnExit += ExitZone;
    }

    private void OnDisable()
    {
        _checkZone.OnEnter -= CheckEnterZone;
        _checkZone.OnExit -= ExitZone;
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (_target == null)
            return;

        _agent.SetDestination(_target.position);
    }

    private void CheckEnterZone(Collider2D collision)
    {
        foreach (string attackLayer in _enemyLayerName)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer(attackLayer))
                continue;

            _target = collision.transform;
        }
    }

    private void ExitZone(Collider2D collision)
    {
        foreach (string attackLayer in _enemyLayerName)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer(attackLayer))
                continue;

            _target = null;
        }
    }
}
