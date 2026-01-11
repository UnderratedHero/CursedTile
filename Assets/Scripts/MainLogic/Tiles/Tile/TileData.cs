using UnityEngine;

public class TileData : MonoBehaviour
{
    [SerializeField] private GameObject _sprite;
    private TileInfo _tileInfo;
    
    public void SetInformation(TileInfo info)
    {
        _tileInfo = info;
        var spriteRenderer = _sprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _tileInfo.Sprite;
    }    
}
