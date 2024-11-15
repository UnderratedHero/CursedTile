using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileDataManager : MonoBehaviour
{
    private static List<TileInfo> _tiles;
    
    public static List<TileInfo> Tiles {  get { return _tiles; } }
    public static TileDataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Initialize(IEnumerable<TileInfo> tiles)
    {
        if (Instance == null)
        {
            var tileDataManager = new GameObject("TileDataManager");
            Instance = tileDataManager.AddComponent<TileDataManager>();
            DontDestroyOnLoad(tileDataManager);
        }

        _tiles = tiles.ToList();
    }

    public void UpdateData(IEnumerable<TileInfo> tiles)
    {
        _tiles = tiles.ToList();
    }
}
