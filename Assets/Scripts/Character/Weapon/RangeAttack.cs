using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private float _damage;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void Shoot(float force)
    {
        var prefab = Instantiate(_prefab, transform.position, transform.rotation);
        var rigidBody = prefab.GetComponent<Rigidbody2D>();
        var bullet = prefab.GetComponent<Bullet>();
        bullet.SetDamage(_damage);
        rigidBody.AddForce(transform.right * force, ForceMode2D.Impulse);
    }
}
