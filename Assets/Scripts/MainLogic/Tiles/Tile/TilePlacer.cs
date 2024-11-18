using System.Collections.Generic;
using UnityEngine;

public class TilePlacer : MonoBehaviour
{
    private Dictionary<int, Vector3> _lockedPositions = new Dictionary<int, Vector3>();

    public void ReturnTile(GameObject tile, Vector3 position)
    {
        var tileData = tile.GetComponent<TileData>();
        if (!_lockedPositions.ContainsKey(tileData.Id))
            return;

        _lockedPositions.Remove(tileData.Id);
        tile.transform.position = position;
    }

    public bool SetTilePosition(GameObject tile, Vector3 position)
    {
        var tileData = tile.GetComponent<TileData>(); 
        if (!_lockedPositions.TryAdd(tileData.Id, position))
            return false;
        
        tile.transform.position = position + new Vector3(0, 0, -1f);
        return true;
    }
}
