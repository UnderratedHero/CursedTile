using System.Linq;
using UnityEngine;

public class TileInfoRandom
{
    private TileType _type;
    private DifficultyLevel _difficulty;
    private Sprite _sprite;

    public Sprite Sprite {  get { return _sprite; } }
    public TileType Type {  get { return _type; } } 
    public DifficultyLevel DifficultyLevel { get { return _difficulty; } }


    public TileInfoRandom() { }

    public TileInfoRandom(TileClusterConfig config)
    {
        RandomInitialize(config);
    }

    private void RandomInitialize(TileClusterConfig config)
    {
        GetRandomTileType(config);
        GetRandomDifficultyLevel(config);
        _sprite = config.Sprites.FirstOrDefault(v => v.tileType == _type).sprite;
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
}
