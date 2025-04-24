using UnityEngine;

public class MapTile : MonoBehaviour
{
    private (int x, int y) _id;
    public (int x, int y) Id { get { return _id; } }

    public void SetId(int x, int y)
    {
        _id.x = x;
        _id.y = y;
    }
}
