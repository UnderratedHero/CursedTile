using System.Collections.Generic;
using UnityEngine;

public class TileCluster : MonoBehaviour
{
    [SerializeField] private int _tilesAmount = 8;
    [SerializeField] private TileClusterConfig _config;
    private List<TileInfoRandom> _tiles;

    public List<TileInfoRandom> Tiles {  get { return _tiles; } }

    private void Awake()
    {
        _tiles = new List<TileInfoRandom>();
        for (var i = 0; i < _tilesAmount; i++)
            _tiles.Add(new TileInfoRandom(_config));
    }
}
