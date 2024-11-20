using UnityEngine;

public class HealSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject _heal;
    public void Spawn()
    {
        var instance = Instantiate(_heal);
        instance.transform.SetParent(transform, true);
        instance.transform.position = transform.position;
    }
}
