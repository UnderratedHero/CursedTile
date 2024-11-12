using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private string _targetLayerName;

    private int _targetLayer;
    private List<Tile> _tiles; 

    private void Awake()
    {
        _targetLayer = LayerMask.NameToLayer(_targetLayerName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != _targetLayer)
            return;
       
        var tileCluster = other.GetComponent<TileCluster>();
        if (tileCluster == null)
            return;

        _tiles = tileCluster.Tiles;
    }
}
