using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DamagePush : MonoBehaviour
{
    [SerializeField] private float _pushPower = 5f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Push(GameObject collision)
    {
        if (collision == null) return;

        Vector2 direction = (transform.position - collision.transform.position).normalized;
        _rb.AddForce(direction * _pushPower, ForceMode2D.Impulse);
    }
}
