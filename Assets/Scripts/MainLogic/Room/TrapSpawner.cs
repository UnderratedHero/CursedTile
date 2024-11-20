using UnityEngine;

public class TrapSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject _trap;
    public void Spawn()
    {
        var instance = Instantiate(_trap);
        instance.transform.SetParent(transform, true);
        instance.transform.position = transform.position;
    }
}
