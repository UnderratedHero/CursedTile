using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewTile", menuName = "Tiles/Custom Tile")]
public class TileForMap : TileBase
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private bool _isSpriteEnable;
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (_isSpriteEnable)
            tileData.sprite = _sprite;

        tileData.flags = TileFlags.LockTransform;
    }
}
