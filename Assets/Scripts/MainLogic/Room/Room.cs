using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject _enterPoint;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private TrapSpawner _trapSpawner;
    [SerializeField] private HealSpawner _healSpawner;

    private int _id;
    private TileInfoRandom _data;

    public int Id { get { return _id; } }
    public TileInfoRandom Data { get { return _data; } }
    public GameObject EnterPoint {  get { return _enterPoint; } }

    public void SetInformation(int id, TileInfoRandom info)
    {
        _id = id++;
        _data = info;
        switch (info.Type)
        {
            case TileType.Enemy:
                _enemySpawner.Spawn();
                break;
            case TileType.Trap:
                _trapSpawner.Spawn();
                break;
            case TileType.Heal:
                _healSpawner.Spawn();
                break;
        }

    }
}
