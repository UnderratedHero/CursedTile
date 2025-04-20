using NavMeshPlus.Components;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] private TileBase _wallTile; 
    [SerializeField] private TileBase _floorTile;
    [SerializeField] private float _tileSize = 1f; 

    private const int NonWalkableArea = 1; 

    private readonly string GridName = "Grid";
    private readonly string NonWalkableTileMapName = "NonWalkableTileMap";
    private readonly string WalkableTileMapName = "WalkableTileMap";
    private readonly string WallTag = "Wall";
    private readonly string FloorTag = "Floor";

    public void GenerateTilemaps(GameObject room)
    {
        var grid = CreateGrid(room);
        GenerateTilemap(room, WallTag, NonWalkableTileMapName, _wallTile, true, grid);
        GenerateTilemap(room, FloorTag, WalkableTileMapName, _floorTile, false, grid);
    }

    private GameObject CreateGrid(GameObject room)
    {
        var gridObject = new GameObject(GridName);
        var grid = gridObject.AddComponent<Grid>();
        grid.cellSize = new Vector3(_tileSize, _tileSize, 0);

        gridObject.transform.SetParent(room.transform, false);
        gridObject.transform.localPosition = Vector3.zero;
        return gridObject;
    }

    private void GenerateTilemap(
        GameObject room,
        string tag,
        string tilemapName,
        TileBase tile,
        bool overrideArea, GameObject gridObject)
    {
        var objects = room.GetComponentsInChildren<Transform>()
                          .Where(t => t.CompareTag(tag))
                          .ToArray();

        if (objects.Length == 0)
        {
            Debug.LogError($"{tag} не найдены! Убедитесь, что они помечены тегом '{tag}'.");
            return;
        }

        var grid = gridObject.GetComponent<Grid>();

        var tilemapObject = new GameObject(tilemapName);
        tilemapObject.transform.SetParent(gridObject.transform);

        var modifier = tilemapObject.AddComponent<NavMeshModifier>();
        modifier.overrideArea = overrideArea;
        if (overrideArea)
            modifier.area = NonWalkableArea;

        var tilemap = tilemapObject.AddComponent<Tilemap>();
        tilemapObject.AddComponent<TilemapRenderer>();
        tilemapObject.transform.position = room.transform.position;
        tilemap.transform.localPosition = Vector3.zero;

        foreach (var obj in objects)
        {
            var cellPosition = grid.WorldToCell(obj.position);
            tilemap.SetTile(cellPosition, tile);
        }
    }
}