using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject _enterPoint;

    private int _id;
    private TileInfoRandom _data;

    public int Id { get { return _id; } }
    public TileInfoRandom Data { get { return _data; } }
    public GameObject EnterPoint {  get { return _enterPoint; } }

    public void SetInformation(int id, TileInfoRandom info)
    {
        _id = id++;
        _data = info;
    }
}
