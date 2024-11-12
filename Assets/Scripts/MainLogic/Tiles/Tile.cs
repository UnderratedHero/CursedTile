using System.Linq;
using UnityEngine;

public class Tile
{
    private TileType _type;
    private DifficultyLevel _difficulty;
    private bool _isGuaranteeFill;
    
    private const float FillConstant = 0.5f;

    public TileType Type {  get { return _type; } } 
    public DifficultyLevel DifficultyLevel { get { return _difficulty; } }
    public bool IsGuaranteeFill { get {  return _isGuaranteeFill; } }


    public Tile(TileClusterConfig config)
    {
        RandomInitialize(config);
    }

    private void RandomInitialize(TileClusterConfig config)
    {
        GetRandomTileType(config);
        GetRandomDifficultyLevel(config);
        GetRandomFill();
    }

    private void GetRandomTileType(TileClusterConfig config)
    {
        var type = config.TileType;
        if (type == null || !type.Any())
            return;

        var randomIndex = Random.Range(0, type.Count);
        _type = type[randomIndex];
    }

    private void GetRandomDifficultyLevel(TileClusterConfig config)
    {
        var difficulty = config.DifficultyLevel;
        if (difficulty == null || !difficulty.Any())
            return;

        var randomIndex = Random.Range(0, difficulty.Count);
        _difficulty = difficulty[randomIndex];
    }

    private void GetRandomFill()
    {
       _isGuaranteeFill = Random.value > FillConstant;
    }
}
