using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _targetName = "Target";
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Health _health;

    private Transform _target;

    void Start()
    {
        var root = transform.parent.parent;

        foreach (var child in root.transform.GetComponentsInChildren<Transform>(true))
        {
            if (!child.name.Contains(_targetName))
                continue;

            _target = child;
        }

        if (_target == null)
            return;
    }

    void Update()
    {
        if (_target == null)
            return;
        
        var direction = (_target.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }
}
