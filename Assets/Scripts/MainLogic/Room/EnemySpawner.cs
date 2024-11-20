using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject _enemy;

    public void Spawn()
    {
        var instance = Instantiate(_enemy);
        instance.transform.SetParent(transform, true);
        instance.transform.position = transform.position;
    }
}
