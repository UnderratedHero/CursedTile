using UnityEngine;

public class TileInputHandler : MonoBehaviour
{
    [SerializeField] private string _targetLayerName;

    private bool _waitingForSecondClick = false;
    private bool _isPlaced = false;
    private int _targetLayer;
    
    private Vector3 _startTransform;
    private GameObject _selectedTile;
    private RoomForming _roomForming;
    private TilePlacer _tilePlacer;
    
    private void OnEnable()
    {
        if (transform.parent == null)
            return;

        _startTransform = transform.position;
        _tilePlacer = transform.parent.GetComponent<TilePlacer>();
        _roomForming = transform.parent.GetComponent<RoomForming>();
        _targetLayer = LayerMask.NameToLayer(_targetLayerName);
    }

    private void Update()
    {
        if (!_waitingForSecondClick || _isPlaced)
            return;

        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.OverlapPoint(ray);

        if (hit == null || hit.gameObject.layer != _targetLayer)
            return;

        var secondObject = hit.gameObject;
        HandleTileInteraction(secondObject);

        _waitingForSecondClick = false;
        _isPlaced = true;
    }

    private void OnMouseDown()
    {
        if (_isPlaced)
        {
            _tilePlacer.ReturnTile(_selectedTile, _startTransform);
            _isPlaced = false;
            return;
        }

        if (_waitingForSecondClick)
            return;

        _selectedTile = gameObject;
        _waitingForSecondClick = true;
    }

    private void HandleTileInteraction(GameObject target)
    {
        if (!_tilePlacer.SetTilePosition(_selectedTile, target.transform.position))
            return;

        _roomForming.AddTile(_selectedTile);
    }
}
