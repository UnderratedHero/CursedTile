using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float _heal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Health>(out var heath))
            return;

        if (heath.EntityHealth == heath.EntityMaxHealth)
            return;

        heath.Heal(_heal);
        Destroy(gameObject);
    }
}
