using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float _heal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Health>(out var heath))
            return;

        heath.Heal(_heal);
        Debug.Log($"Healed {_heal} HP");
        Destroy(gameObject);
    }
}
