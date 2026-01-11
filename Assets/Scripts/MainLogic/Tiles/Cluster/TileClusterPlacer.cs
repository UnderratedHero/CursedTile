using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileClusterPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private float _tilePositionStep;
    [SerializeField] private Vector3 _leftEnd;
    [SerializeField] private Vector3 _rightEnd;

    private List<TileInfo> _tiles;
    private List<Vector3> _tilePositions;

    private void Start()
    {
        GeneratePositions();
        SpawnTiles();
    }

    private void GeneratePositions()
    {
        _tiles = TileDataManager.Tiles;
        _tilePositions = new List<Vector3>();
        var direction = (_rightEnd - _leftEnd).normalized;

        float totalDistance = Vector3.Distance(_leftEnd, _rightEnd);
        float distancePerStep = totalDistance / (_tiles.Count - 1);
        for (int i = 0; i < _tiles.Count; i++)
        {
            var newPosition = _leftEnd + direction * distancePerStep * i;
            _tilePositions.Add(newPosition);
        }
    }

    private void SpawnTiles()
    {
        if (!_tiles.Any() || !_tilePositions.Any())
            return;

        for (int i = 0; i < _tiles.Count; i++)
        {
            var tile = _tiles[i];
            var position = _tilePositions[i];
            var tileInstance = Instantiate(_tilePrefab, position, Quaternion.identity, transform);

            var data = tileInstance.GetComponent<TileData>();
            data.SetInformation(tile);
        }
    }
}