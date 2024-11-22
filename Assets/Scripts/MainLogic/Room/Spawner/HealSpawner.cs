using UnityEngine;

public class HealSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject _heal;
    private GameObject _instance;

    public GameObject Instance { get { return _instance; } }

    public void Spawn()
    {
        _instance = Instantiate(_heal);
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
