using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject _enterPoint;
    [SerializeField] private GameObject _spawnerObject;
    [SerializeField] private GameObject _exitPoint;
    private ISpawner _spawner;

    private int _id;
    private TileInfoRandom _data;

    public int Id { get { return _id; } }
    public GameObject EnterPoint {  get { return _enterPoint; } }
    public GameObject ExitPoint { get { return _exitPoint; } }

    public void SetInformation(int id, TileInfoRandom info)
    {
        _id = id++;
        _data = info;
        switch (info.Type)
        {
            case TileType.Enemy:
                _spawner = _spawnerObject.GetComponent<EnemySpawner>();
                break;
            case TileType.Trap:
                _spawner = _spawnerObject.GetComponent<TrapSpawner>();
                break;
            case TileType.Heal:
                _spawner = _spawnerObject.GetComponent<HealSpawner>();
                _exitPoint.SetActive(true);
                break;
            default:
                return;
        }

        _spawner.Spawn();
        
        if (_id != 0)
            _spawner.UnActive();
    }

    public void SetActiveSpawner()
    {
        _spawner.SetActive();
    }
}
