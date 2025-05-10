using UnityEngine;
using UnityEngine.AI;

public class Judge : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private Character _target;

    private void OnEnable()
    {
        _target = FindObjectOfType<Character>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (_target == null)
            return;

        _agent.SetDestination(_target.gameObject.transform.position);
    }
}
