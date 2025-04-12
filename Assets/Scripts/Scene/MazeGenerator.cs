using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] private GameObject _enterPrefab;
    [SerializeField] private GameObject _exitPrefab;
    [SerializeField] private GameObject _torchPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _trapPrefab;
    [SerializeField] private GameObject _healPrefab;

    [SerializeField] private int _mazeWidth = 10;
    [SerializeField] private int _mazeHeight = 10;

    [SerializeField] private float _tileSize = 1f;

    [Range(0, 100)]
    [SerializeField] private int _torchSpawnChance = 50;

    [Range(0, 100)]
    [SerializeField] private float _enemySpawnChance = 20f;

    [Range(0, 100)]
    [SerializeField] private float _trapSpawnChance = 15f;

    [Range(0, 100)]
    [SerializeField] private float _healSpawnChance = 10f;

    private int[,] _mazeGrid;
    private bool[,] _visited;
    private Vector2[] _directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    private List<Vector2Int> _floorTiles = new List<Vector2Int>();

    public GameObject Enter { get { return _enterPrefab; } }
    public GameObject Exit { get { return _exitPrefab; } }

    public void GenerateMaze(Room room)
    {
        var exit = _exitPrefab.GetComponent<RoomExit>();
        exit.SetRoom(room);

        _mazeGrid = new int[_mazeWidth, _mazeHeight];
        _visited = new bool[_mazeWidth, _mazeHeight];

        for (var x = 0; x < _mazeWidth; x++)
        {
            for (var y = 0; y < _mazeHeight; y++)
            {
                _mazeGrid[x, y] = 1;
            }
        }

        for (var x = 0; x < _mazeWidth; x++)
        {
            _mazeGrid[x, 0] = 1;
            _mazeGrid[x, _mazeHeight - 1] = 1;
        }
        for (var y = 0; y < _mazeHeight; y++)
        {
            _mazeGrid[0, y] = 1;
            _mazeGrid[_mazeWidth - 1, y] = 1;
        }

        CarvePassagesFrom(1, 1);

        DrawMaze();

        SpawnEnterAndExit();
        SpawnTorchesInCorners();
        SpawnEnemies();
        SpawnHeals();
        SpawnTraps();
    }

    private void CarvePassagesFrom(int startX, int startY)
    {
        _visited[startX, startY] = true;
        _mazeGrid[startX, startY] = 0;

        _floorTiles.Add(new Vector2Int(startX, startY));

        Shuffle(_directions);

        foreach (var direction in _directions)
        {
            var newX = startX + (int)direction.x * 2;
            var newY = startY + (int)direction.y * 2;

            if (newX >= 1 && newX < _mazeWidth - 1 && newY >= 1 && newY < _mazeHeight - 1 && !_visited[newX, newY])
            {
                _mazeGrid[startX + (int)direction.x, startY + (int)direction.y] = 0;

                CarvePassagesFrom(newX, newY);
            }
        }
    }

    private void DrawMaze()
    {
        var roomPosition = transform.position;

        for (var x = 0; x < _mazeWidth; x++)
        {
            for (var y = 0; y < _mazeHeight; y++)
            {
                var position = new Vector3(
                    roomPosition.x + x * _tileSize,
                    roomPosition.y + y * _tileSize,
                    roomPosition.z
                );

                if (_mazeGrid[x, y] == 1)
                {
                    Instantiate(_wallPrefab, position, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(_floorPrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }

    private void SpawnEnterAndExit()
    {
        var roomPosition = transform.position;

        var firstFloorTile = _floorTiles.First();
        var playerSpawnPosition = new Vector3(
            roomPosition.x + firstFloorTile.x * _tileSize,
            roomPosition.y + firstFloorTile.y * _tileSize,
            roomPosition.z
        );
        Instantiate(_enterPrefab, playerSpawnPosition, Quaternion.identity, transform);

        var lastFloorTile = _floorTiles.Last();
        var exitPosition = new Vector3(
            roomPosition.x + lastFloorTile.x * _tileSize,
            roomPosition.y + lastFloorTile.y * _tileSize,
            roomPosition.z
        );
        Instantiate(_exitPrefab, exitPosition, Quaternion.identity, transform);
    }

    private void SpawnTorchesInCorners()
    {
        var roomPosition = transform.position;

        foreach (var tile in _floorTiles)
        {
            if (IsCorner(tile.x, tile.y))
            {
                var randomValue = UnityEngine.Random.Range(0, 100);

                if (randomValue <= _torchSpawnChance)
                {
                    var torchPosition = new Vector3(
                        roomPosition.x + tile.x * _tileSize,
                        roomPosition.y + tile.y * _tileSize,
                        roomPosition.z
                    );

                    Instantiate(_torchPrefab, torchPosition, Quaternion.identity, transform);
                }
            }
        }
    }

    private void SpawnEnemies()
    {
        var roomPosition = transform.position;

        foreach (var tile in _floorTiles)
        {
            var randomValue = UnityEngine.Random.Range(0, 100);

            if (randomValue <= _enemySpawnChance)
            {
                var enemyPosition = new Vector3(
                    roomPosition.x + tile.x * _tileSize,
                    roomPosition.y + tile.y * _tileSize,
                    roomPosition.z
                );

                Instantiate(_enemyPrefab, enemyPosition, Quaternion.identity, transform);
            }
        }
    }

    private void SpawnHeals()
    {
        var roomPosition = transform.position;

        foreach (var tile in _floorTiles)
        {
            var randomValue = UnityEngine.Random.Range(0, 100);

            if (randomValue <= _healSpawnChance)
            {
                var healPosition = new Vector3(
                    roomPosition.x + tile.x * _tileSize,
                    roomPosition.y + tile.y * _tileSize,
                    roomPosition.z
                );

                Instantiate(_healPrefab, healPosition, Quaternion.identity, transform);
            }
        }
    }

    private void SpawnTraps()
    {
        var roomPosition = transform.position;

        foreach (var tile in _floorTiles)
        {
            var randomValue = UnityEngine.Random.Range(0, 100);

            if (randomValue <= _trapSpawnChance)
            {
                var trapPosition = new Vector3(
                    roomPosition.x + tile.x * _tileSize,
                    roomPosition.y + tile.y * _tileSize,
                    roomPosition.z
                );

                Instantiate(_trapPrefab, trapPosition, Quaternion.identity, transform);
            }
        }
    }

    private bool IsCorner(int x, int y)
    {
        var isTopLeftCorner = _mazeGrid[x, y + 1] == 1 && _mazeGrid[x - 1, y] == 1;
        var isTopRightCorner = _mazeGrid[x, y + 1] == 1 && _mazeGrid[x + 1, y] == 1;
        var isBottomLeftCorner = _mazeGrid[x, y - 1] == 1 && _mazeGrid[x - 1, y] == 1;
        var isBottomRightCorner = _mazeGrid[x, y - 1] == 1 && _mazeGrid[x + 1, y] == 1;

        return isTopLeftCorner || isTopRightCorner || isBottomLeftCorner || isBottomRightCorner;
    }

    private void Shuffle(Vector2[] array)
    {
        for (var i = array.Length - 1; i > 0; i--)
        {
            var j = UnityEngine.Random.Range(0, i + 1);
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}