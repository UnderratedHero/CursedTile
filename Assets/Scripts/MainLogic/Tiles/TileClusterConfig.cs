using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileConfig", menuName = "Config/TileConfig", order = 51)]
public class TileClusterConfig : ScriptableObject
{
    [SerializeField] private List<TileType> _tileType;
    [SerializeField] private List<DifficultyLevel> _difficultyLevel;
    
    public List<TileType> TileType { get { return _tileType; } }
    public List<DifficultyLevel> DifficultyLevel { get { return _difficultyLevel; } }
}
