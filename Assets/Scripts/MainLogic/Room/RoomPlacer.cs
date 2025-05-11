using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _roomPrefab;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _judge;
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private Vector3 _leftEnd;
    [SerializeField] private Vector3 _rightEnd;
    [SerializeField] private float _minutesToWait = 1;
    [SerializeField] private Timer _timer;


    private List<TileInfoRandom> _tilesInfo;
    private List<Vector3> _roomPositions;
    private List<Room> _rooms;
    private Character _character;
    private List<Coroutine> _coroutines;

    public List<Room> Rooms { get { return _rooms; } }

    private void Awake()
    {
        GeneratePositions();
        SpawnRooms();
        SpawnCharacter();

       
    }

    private void Start()
    {
        BakeSurface();
    }

    private void BakeSurface()
    {
        _surface.RemoveData();
        _surface.BuildNavMesh();

        foreach (var room in _rooms)
            room.SpawnEnemies();
    }

    private void GeneratePositions()
    {
        _tilesInfo = TileDataManager.Tiles;
        _roomPositions = new List<Vector3>();
        _rooms = new List<Room>();
        var direction = (_rightEnd - _leftEnd).normalized;

        float totalDistance = Vector3.Distance(_leftEnd, _rightEnd);
        float distancePerStep = totalDistance / (_tilesInfo.Count - 1);
        for (int i = 0; i < 6; i++)
        {
            var newPosition = _leftEnd + direction * distancePerStep * i;
            _roomPositions.Add(newPosition);
        }
    }

    private void SpawnRooms()
    {
        if (!_roomPositions.Any())
            return;

        for (int i = 0; i < 6; i++)
        {
            var tile = new TileInfoRandom();

            if (_tilesInfo.Count > i && _tilesInfo[i] != null)
                tile = _tilesInfo[i];

            var position = _roomPositions[i];
            var room = Instantiate(_roomPrefab, position, Quaternion.identity, transform);

            var roomData = room.GetComponent<Room>();

            if (tile != null)
                roomData.SetInformation(i, tile);
            else
                roomData.SetInformation(i);

            roomData.GenerateMaze();
            _rooms.Add(roomData);
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

        var player = Instantiate(_characterPrefab, spawnPosition, Quaternion.identity, transform);
        player.SetActive(true);
        if (!player.TryGetComponent(out _character))
            return;

        _character.SetCurrentRoomId(room.Id);
        StartCoroutine(WaitAndExecute());
    }

    private IEnumerator WaitAndExecute()
    {
        float secondsToWait = _minutesToWait * 60f;
        yield return new WaitForSeconds(secondsToWait);

        SpawnJudge();
    }

    private void SpawnJudge()
    {
        var room = _rooms.FirstOrDefault(v => v.Id == _character.CurrentRoomId);
        if (room == null)
        {
            return;
        }

        Transform enterTransform = null;
        foreach (var child in room.gameObject.transform.GetComponentsInChildren<Transform>(true))
        {
            if (!child.name.Contains("Enter"))
                continue;

            enterTransform = child;
            break;
        }

        var spawnPosition = enterTransform.position;

        var judge = Instantiate(_judge, spawnPosition, Quaternion.identity, transform);
        judge.SetActive(true);
        room.SetJudge(judge);
    }

    public void ResetJudge()
    {
        var room = _rooms.FirstOrDefault(v => v.Id == _character.CurrentRoomId - 1);
        if (room == null)
        {
            return;
        }

        StopAllCoroutines();

        room.ResetJudge();
        _timer.RestartTimer();
        StartCoroutine(WaitAndExecute());
    }
}
