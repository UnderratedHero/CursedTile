using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] private float _moveTimeDelay = 3f;
    [SerializeField] private float _range = 5f;

    private NavMeshAgent _agent;
    private float _timer = 0f;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetNewDestination();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _moveTimeDelay)
        {
            _timer = 0f;
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        if (RandomAxisPoint(transform.position, _range, out var point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            _agent.SetDestination(point);
        }
    }

    private bool RandomAxisPoint(Vector3 center, float range, out Vector3 result)
    {
        bool isHorizontal = Random.value > 0.5f;

        Vector3 randomPoint;
        if (isHorizontal)
        {
            randomPoint = new Vector3(
                center.x + Random.Range(-range, range),
                center.y,                             
                center.z
            );
        }
        else
        {
            randomPoint = new Vector3(
                center.x,
                center.y + Random.Range(-range, range), 
                center.z
            );
        }

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}