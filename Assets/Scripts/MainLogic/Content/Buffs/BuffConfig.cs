using UnityEngine;

[CreateAssetMenu(fileName = "BuffConfig", menuName = "Config/BuffConfig", order = 51)]
public class BuffConfig : ScriptableObject
{
    [SerializeField] private BuffType _type;
    [SerializeField] private int _points;
    [SerializeField] private int _duration;
    [SerializeField] private int _increase;
    [SerializeField] private int _cost;
    [SerializeField] private Color _color;
    [SerializeField] private string _buffName;

    public BuffType Type => _type;
    public int Points => _points;
    public int Duration => _duration;
    public int Increase => _increase;
    public int Cost => _cost;
    public Color Color => _color;
    public string BuffName => _buffName;
}
