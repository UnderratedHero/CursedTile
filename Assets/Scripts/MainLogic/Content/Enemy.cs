using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _targetName = "Target";
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Health _health;

    private GameObject _exit;
    private Transform _target;

    private void OnEnable()
    {
        _health.OnEntityDead += SetActive;
    }

    void Start()
    {
        var targetObject = GameObject.Find(_targetName);

        if (targetObject == null)
            return;

        _target = targetObject.transform;
        var parentTransform = transform.parent.parent;
        var room = parentTransform.GetComponent<Room>();
        _exit = room.ExitPoint;
    }

    void Update()
    {
        if (_target == null)
            return;
        
        var direction = (_target.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    private void OnDestroy()
    {
        _health.OnEntityDead -= SetActive;
    }

    private void SetActive()
    {
        _exit.SetActive(true);
    }
}
