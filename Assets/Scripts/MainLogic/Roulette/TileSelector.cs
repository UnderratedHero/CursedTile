using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    [SerializeField] private string _targetLayerName;
    [SerializeField] private string _sceneName;
    
    private int _targetLayer;
    private List<TileInfoRandom> _tiles; 
    
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
        TileDataManager.Initialize(_tiles);
        SceneTransition.SwitchToScene(_sceneName);
    }
}
