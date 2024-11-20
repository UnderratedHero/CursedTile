using Assets.Scripts.Character;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour, IControllable
{
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _character;
    [SerializeField] private MeleeAttack _attackArea;
    [SerializeField] private RangeAttack _rangeAttack;
    [SerializeField] private List<WeaponConfig> _weaponConfigs;
    private WeaponConfig _currentWeaponConfig;

    private void Start()
    {
        _currentWeaponConfig = _weaponConfigs.First();
        _weapon.gameObject.SetActive(true);
        _weapon.SetSprite(_currentWeaponConfig.WeaponSprite);
    }

    public void SwitchWeapon()
    {
        _currentWeaponConfig = _weaponConfigs.FirstOrDefault(v => v.Id != _currentWeaponConfig.Id);
        _weapon.SetSprite(_currentWeaponConfig.WeaponSprite);
    }

    public void Attack()
    {
        switch (_currentWeaponConfig.WeaponType)
        {
            case WeaponType.Melee:
                _attackArea.SetDamage(_currentWeaponConfig.Damage);
                _attackArea.gameObject.SetActive(true);
                break;
            case WeaponType.Range:
                _rangeAttack.gameObject.SetActive(true);
                _rangeAttack.Shoot(_currentWeaponConfig.Damage, _currentWeaponConfig.AttackSpeed);
                break;
            case WeaponType.Magic:
                break;
        }
    }

    public void Move(Vector2 vector)
    {
        _character.transform.position += new Vector3(vector.x, vector.y) * _moveSpeed * Time.deltaTime;
    }

    public void Unack()
    {
        switch (_currentWeaponConfig.WeaponType)
        {
            case WeaponType.Melee:
                _attackArea.gameObject.SetActive(false);
                break;
            case WeaponType.Range:
                _rangeAttack.gameObject.SetActive(false);
                break;
            case WeaponType.Magic:
                break;
        }
    }
}
