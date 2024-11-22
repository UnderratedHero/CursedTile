using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _roomPrefab;
    [SerializeField] private float _roomPositionStep;
    [SerializeField] private Vector3 _leftEnd;
    [SerializeField] private Vector3 _rightEnd;

    private List<TileInfoRandom> _tilesInfo;
    private List<Vector3> _roomPositions;
    private List<Room> _rooms;

    public List<Room> Rooms { get { return _rooms; } }

    private void Start()
    {
        GeneratePositions();
        SpawnRooms();
    }

    private void GeneratePositions()
    {
        _tilesInfo = TileDataManager.Tiles;
        _roomPositions = new List<Vector3>();
        _rooms = new List<Room>();
        var direction = (_rightEnd - _leftEnd).normalized;

        float totalDistance = Vector3.Distance(_leftEnd, _rightEnd);
        float distancePerStep = totalDistance / (_tilesInfo.Count - 1);
        for (int i = 0; i < _tilesInfo.Count; i++)
        {
            var newPosition = _leftEnd + direction * distancePerStep * i;
            _roomPositions.Add(newPosition);
        }
    }

    private void SpawnRooms()
    {
        if (!_tilesInfo.Any() || !_roomPositions.Any())
            return;

        for (int i = 0; i < _tilesInfo.Count; i++)
        {
            var tile = _tilesInfo[i];
            var position = _roomPositions[i];
            var room = Instantiate(_roomPrefab, position, Quaternion.identity, transform);

            var data = room.GetComponent<Room>();
            data.SetInformation(i, tile);
            _rooms.Add(data);
        }
    }
}
