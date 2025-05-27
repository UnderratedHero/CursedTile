using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private MazeGenerator _mazeGenerator;


    private int _id;
    private TileInfoRandom _data;
    private GameObject _judge;

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
        else
            _data = new TileInfoRandom();
    }

    public void GenerateMaze()
    {
        _mazeGenerator.GenerateMaze(this);
    }

    public void ResetJudge()
    {
        if (_judge != null)
            Destroy(_judge);
    }

    public void SetJudge(GameObject judge)
    {
        _judge = judge; 
    }
}
