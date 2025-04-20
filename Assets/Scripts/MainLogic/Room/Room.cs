using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private MazeGenerator _mazeGenerator;

    private int _id;
    private TileInfoRandom _data;

    public int Id { get { return _id; } }

    public void SpawnEnemies()
    {
        _mazeGenerator.SpawnEnemies();
    }

    public void SetInformation(int id, TileInfoRandom info)
    {
        _id = id;
        _data = info;
        _mazeGenerator.GenerateMaze(this);
    }
}
