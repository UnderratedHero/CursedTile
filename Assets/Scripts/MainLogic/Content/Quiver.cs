using UnityEngine;

public class Quiver : MonoBehaviour
{
    [SerializeField] private int _arrows;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Character>(out var character))
            return;

        character.AddArrows(_arrows);
        Destroy(gameObject);
    }
}
