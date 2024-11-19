using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _sprite;
    private TileInfoRandom _tileInfo;
    private int _id;

    public TileInfoRandom TileInfo { get { return _tileInfo; } }
    public int Id { get { return _id; } }

    public void SetInformation(int id, TileInfoRandom info)
    {
        _id = id++;
        _tileInfo = info;
        var spriteRenderer = _sprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _tileInfo.Sprite;
    }    
}
