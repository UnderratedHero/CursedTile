using System.Collections.Generic;
using UnityEngine;

public class TileCluster : MonoBehaviour
{
    [SerializeField] private int _tilesAmount = 8;
    [SerializeField] private TileClusterConfig _config;
    private List<Tile> _tiles;

    private void Awake()
    {
        _tiles = new List<Tile>();
        for (var i = 0; i < _tilesAmount; i++)
            _tiles.Add(new Tile(_config));
    }
}
