using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
