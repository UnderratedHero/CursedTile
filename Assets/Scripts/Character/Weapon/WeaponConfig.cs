using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Config/WeaponConfig", order = 51)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Sprite _weaponSprite;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;

    public int Id { get { return _id; } }
    public WeaponType WeaponType { get { return _weaponType; } }
    public Sprite WeaponSprite { get { return _weaponSprite; } }
    public float Damage { get { return _damage; } }
    public float AttackSpeed { get { return _attackSpeed; } }
}


