using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _targetName = "Target";
    [SerializeField] private Health _health;

    private Transform _target;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        var root = transform.parent.parent;

        foreach (var child in root.transform.GetComponentsInChildren<Transform>(true))
        {
            if (!child.name.Contains(_targetName))
                continue;

            _target = child;
        }
    }

    void Update()
    {
        if (_target == null)
            return;

        _agent.SetDestination(_target.position);
    }
}
