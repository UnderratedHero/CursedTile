using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _roomPrefab;
    [SerializeField] private GameObject _character;
    [SerializeField] private float _roomPositionStep;
    [SerializeField] private Vector3 _leftEnd;
    [SerializeField] private Vector3 _rightEnd;

    private List<TileInfoRandom> _tilesInfo;
    private List<Vector3> _roomPositions;
    private List<Room> _rooms;

    public List<Room> Rooms { get { return _rooms; } }

    private void Awake()
    {
        GeneratePositions();
        SpawnRooms();
        SpawnCharacter();
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

    private void SpawnCharacter()
    {
        var room = _rooms.FirstOrDefault(v => v.Id == 0);
        if (room == null)
        {
            Debug.LogError("Комната с Id == 0 не найдена.");
            return;
        }

        var roomObject = transform.Find(room.gameObject.name)?.gameObject;
        if (roomObject == null)
        {
            Debug.LogError($"Объект комнаты {room.gameObject.name} не найден в иерархии RoomPlacer.");
            return;
        }

        Transform enterTransform = null;
        foreach (var child in roomObject.transform.GetComponentsInChildren<Transform>(true))
        {
            if (!child.name.Contains("Enter"))
                continue;

            enterTransform = child;
            break;
        }

        var spawnPosition = enterTransform.position;

        var player = Instantiate(_character, spawnPosition, Quaternion.identity, transform);
        player.SetActive(true);
    }
}
