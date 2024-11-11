using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileConfig", menuName = "Config/TileConfig", order = 51)]
public class TileClusterConfig : ScriptableObject
{
    [SerializeField] private List<TileType> _tileType;
    [SerializeField] private List<DifficultyLevel> _difficultyLevel;
    [SerializeField] private bool _isGuaranteeFill;       
}
