using System.Collections.Generic;
using UnityEngine;

public class RoomForming : MonoBehaviour
{
    private List<GameObject> _tiles;

    private void Awake()
    {
        _tiles = new List<GameObject>();
    }

    public void AddTile(GameObject tile)
    {
        _tiles.Add(tile);
    }

    public void ApplyTiles()
    {
        var result = new List<TileInfoRandom>();

        foreach (var tile in _tiles)
        {
            var data = tile.GetComponent<Tile>();
            result.Add(data.TileInfo);
        }

        TileDataManager.UpdateData(result);
    }
}
