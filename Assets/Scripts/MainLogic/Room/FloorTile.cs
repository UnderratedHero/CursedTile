using UnityEngine;

public class FloorTile : MonoBehaviour
{
    private int _x;
    private int _y;

    public void SetId(int x, int y)
    {
        _x = x;
        _y = y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            character.SetCurrentTile(_x, _y);
        }
    }
}
