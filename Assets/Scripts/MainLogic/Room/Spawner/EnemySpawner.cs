using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject _enemy;
    private GameObject _instance;

    public GameObject Instance {  get { return _instance; } }

    public void Spawn()
    {
        _instance = Instantiate(_enemy);
        _instance.transform.SetParent(transform, true);
        _instance.transform.position = transform.position;
    }

    public void UnActive()
    {
        _instance.SetActive(false);
    }

    public void SetActive()
    {
        _instance.SetActive(true);
    }
}
