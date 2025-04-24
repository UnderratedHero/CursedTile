using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoomCellsGenerator : MonoBehaviour
{
    [SerializeField] private int _tilesAmount;
    [SerializeField] private MapTile _tile;
    [SerializeField] private Color _visibleColor;
    [SerializeField] private Color _unVisibleColor;
    [SerializeField] private Color _currentColor;

    private bool _isGenerated = false;

    private void Start()
    {
        if (!_isGenerated)
        {
            for (var i = 0; i < _tilesAmount; i++)
            {
                for (var j = 0; j < _tilesAmount; j++)
                {
                    var tile = Instantiate(_tile, transform.position, Quaternion.identity, transform);
                    tile.gameObject.SetActive(false);
                    tile.SetId(i, j);
                }
            }
            _isGenerated = true;
        }
    }

    private void OnEnable()
    {
        var character = FindObjectOfType<Character>();
        var examinedTiles = character.ExaminedTiles;
        var currentTileId = character.CurrentFloorTileId;

        foreach (Transform children in transform.GetComponentInChildren<Transform>())
        {
            children.gameObject.SetActive(true);
            children.TryGetComponent(out MapTile mapTile);

            if (!examinedTiles.Any(v => v == mapTile.Id))
                children.GetComponent<Image>().color = _unVisibleColor;
            else
                children.GetComponent<Image>().color = _visibleColor;

            if (mapTile.Id == currentTileId)
                children.GetComponent<Image>().color = _currentColor;
        }
    }

    private void OnDisable()
    {
        foreach (Transform children in transform.GetComponentInChildren<Transform>())
        {
            children.gameObject.SetActive(false);
            children.GetComponent<Image>().color = Color.white;
        }
    }
}
