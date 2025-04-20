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
    [SerializeField] private GameObject _casinoPrefab;

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
    [SerializeField] private float _casinoSpawnChance = 10f;

    private int[,] _mazeGrid;
    private bool[,] _visited;
    private Vector2[] _directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    private List<(int Column, int Row)> _occupaedTiles;

    private List<Vector2Int> _floorTiles = new List<Vector2Int>();

    public void GenerateMaze(Room room)
    {
        _occupaedTiles = new List<(int Column, int Row)>();
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
                     roomPosition.x + x * _tileSize - _tileSize / 2f,
                     roomPosition.y + y * _tileSize - _tileSize / 2f,
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

        var startTile = _floorTiles.First();
        var farthestTile = FindFarthestTile(startTile.x, startTile.y);

        var playerSpawnPosition = new Vector3(
            roomPosition.x + startTile.x * _tileSize - _tileSize / 2f,
            roomPosition.y + startTile.y * _tileSize - _tileSize / 2f,
            roomPosition.z
        );
        Instantiate(_enterPrefab, playerSpawnPosition, Quaternion.identity, transform);
        _occupaedTiles.Add((startTile.x, startTile.y));

        var exitPosition = new Vector3(
            roomPosition.x + farthestTile.x * _tileSize - _tileSize / 2f,
            roomPosition.y + farthestTile.y * _tileSize - _tileSize / 2f,
            roomPosition.z
        );
        Instantiate(_exitPrefab, exitPosition, Quaternion.identity, transform);
        _occupaedTiles.Add((farthestTile.x, farthestTile.y));
    }

    private Vector2Int FindFarthestTile(int startX, int startY)
    {
        var queue = new Queue<Vector2Int>();
        var distances = new Dictionary<Vector2Int, int>();
        queue.Enqueue(new Vector2Int(startX, startY));
        distances[new Vector2Int(startX, startY)] = 0;

        var farthestTile = new Vector2Int(startX, startY);
        var maxDistance = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var currentDistance = distances[current];

            foreach (var direction in _directions)
            {
                var nextX = current.x + (int)direction.x;
                var nextY = current.y + (int)direction.y;

                var nextTile = new Vector2Int(nextX, nextY);

                if (_mazeGrid[nextX, nextY] == 0 && !distances.ContainsKey(nextTile))
                {
                    distances[nextTile] = currentDistance + 1;
                    queue.Enqueue(nextTile);

                    if (currentDistance + 1 > maxDistance)
                    {
                        maxDistance = currentDistance + 1;
                        farthestTile = nextTile;
                    }
                }
            }
        }

        return farthestTile;
    }

    private void SpawnTorchesInCorners()
    {
        var roomPosition = transform.position;

        foreach (var tile in _floorTiles)
        {
            if (!IsCorner(tile.x, tile.y))
                continue;

            if (IsOccupied(tile.x, tile.y))
                continue;

            var randomValue = Random.Range(0, 100);

            if (randomValue > _torchSpawnChance)
                continue;

            var torchPosition = new Vector3(
                   roomPosition.x + tile.x * _tileSize - _tileSize / 2f,
                   roomPosition.y + tile.y * _tileSize - _tileSize / 2f,
                   roomPosition.z
               );

            Instantiate(_torchPrefab, torchPosition, Quaternion.identity, transform);
            _occupaedTiles.Add((tile.x, tile.y));
        }
    }

    public void SpawnEnemies()
    {
        var roomPosition = transform.position;

        foreach (var tile in _floorTiles)
        {
            if (IsOccupied(tile.x, tile.y))
                continue;

            var randomValue = Random.Range(0, 100);

            if (randomValue > _enemySpawnChance)
                continue;

            var enemyPosition = new Vector3(
                roomPosition.x + tile.x * _tileSize - _tileSize / 2f,
                roomPosition.y + tile.y * _tileSize - _tileSize / 2f,
                roomPosition.z
            );

            Instantiate(_enemyPrefab, enemyPosition, Quaternion.identity, transform);
            _occupaedTiles.Add((tile.x, tile.y));
        }
    }

    private void SpawnHeals()
    {
        var roomPosition = transform.position;

        foreach (var tile in _floorTiles)
        {
            if (IsOccupied(tile.x, tile.y))
                continue;

            var randomValue = Random.Range(0, 100);

            if (randomValue > _casinoSpawnChance)
                continue;

            var healPosition = new Vector3(
                roomPosition.x + tile.x * _tileSize - _tileSize / 2f,
                roomPosition.y + tile.y * _tileSize - _tileSize / 2f,
                roomPosition.z
            );

            Instantiate(_casinoPrefab, healPosition, Quaternion.identity, transform);
            _occupaedTiles.Add((tile.x, tile.y));
        }
    }

    private void SpawnTraps()
    {
        var roomPosition = transform.position;

        foreach (var tile in _floorTiles)
        {
            if (IsOccupied(tile.x, tile.y))
                continue;

            var randomValue = Random.Range(0, 100);

            if (randomValue > _trapSpawnChance)
                continue;

            var trapPosition = new Vector3(
                roomPosition.x + tile.x * _tileSize - _tileSize / 2f,
                roomPosition.y + tile.y * _tileSize - _tileSize / 2f,
                roomPosition.z
            );

            Instantiate(_trapPrefab, trapPosition, Quaternion.identity, transform);
            _occupaedTiles.Add((tile.x, tile.y));
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

    private bool IsOccupied(int x, int y)
    {
        if (_occupaedTiles.Any(v => v.Column == x && v.Row == y))
            return true;

        return false;
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