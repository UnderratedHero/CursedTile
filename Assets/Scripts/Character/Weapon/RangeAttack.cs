using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    public void Shoot(float damage, float force)
    {
        var prefab = Instantiate(_prefab, transform.position, transform.rotation);
        var rigidBody = prefab.GetComponent<Rigidbody2D>();
        var bullet = prefab.GetComponent<Bullet>();
        bullet.SetDamage(damage);
        rigidBody.AddForce(transform.right * force, ForceMode2D.Impulse);
    }
}
