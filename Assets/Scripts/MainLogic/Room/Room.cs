using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private MazeGenerator _mazeGenerator;

    private int _id;
    private TileInfoRandom _data;

    public int Id { get { return _id; } }
    public TileInfoRandom Data { get { return _data; } }

    public void SpawnEnemies()
    {
        _mazeGenerator.SpawnEnemies();
    }

    public void SetInformation(int id, TileInfoRandom info = null)
    {
        _id = id;
        if (info != null) 
            _data = info;
    }

    public void GenerateMaze()
    {
        _mazeGenerator.GenerateMaze(this);
    }
}
