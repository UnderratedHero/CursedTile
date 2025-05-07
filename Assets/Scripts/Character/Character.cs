using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Character : MonoBehaviour, IControllable
{
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _character;
    [SerializeField] private MeleeAttack _attackArea;
    [SerializeField] private RangeAttack _rangeAttack;
    [SerializeField] private List<WeaponConfig> _weaponConfigs;
    [SerializeField] private float _dashForce = 10f;
    [SerializeField] private float _dashCooldown = 1f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Health _health;
    [SerializeField] private Light2D _light;
    [SerializeField] private PlayerAnimationControl _playerAnimationControl;

    private List<(int x, int y)> _examinedTiles = new List<(int x, int y)>();
    private (int x, int y) _currentFloorTileId;
    private WeaponConfig _currentWeaponConfig;
    private bool _isDashing = false; 
    private bool _isBuffed = false;
    private float _lastDashTime = 0f;
    private int _arrowsAmount = 10;

    public Health Health { get { return _health; } }

    public float LastDashTime { get { return _lastDashTime; } }
    public bool IsDashing { get { return _isDashing; } }
    public float DashCooldown {  get { return _dashCooldown; } }
    public List<(int x, int y)> ExaminedTiles {  get { return _examinedTiles; } }
    public (int x, int y) CurrentFloorTileId { get { return _currentFloorTileId; } }
    public int ArrowsAmount { get { return _arrowsAmount; } }   


    private void Start()
    {
        _currentWeaponConfig = _weaponConfigs.First();
        _weapon.gameObject.SetActive(true);
        _weapon.SetSprite(_currentWeaponConfig.WeaponSprite);
        _attackArea.SetDamage(_currentWeaponConfig.Damage);
        _rangeAttack.SetDamage(_currentWeaponConfig.Damage);
    }

    public void AddArrows(int arrows)
    {
        _arrowsAmount += arrows;
    }

    public void SetCurrentTile(int x, int y)
    {
        _currentFloorTileId.x = x;
        _currentFloorTileId.y = y;
        
        if (!_examinedTiles.Any(v => v == _currentFloorTileId))
            _examinedTiles.Add(_currentFloorTileId);
    }

    public void ClearExaminedTiles()
    {
        _examinedTiles = new List<(int x, int y)>();
        _currentFloorTileId = (-1, -1);
    }

    public void SwitchWeapon()
    {   
        _currentWeaponConfig = _weaponConfigs.FirstOrDefault(v => v.Id != _currentWeaponConfig.Id);
        _weapon.SetSprite(_currentWeaponConfig.WeaponSprite);

        switch (_currentWeaponConfig.WeaponType)
        {
            case WeaponType.Melee:
                if (!_isBuffed)
                    _attackArea.SetDamage(_currentWeaponConfig.Damage);
                break;
            case WeaponType.Range:
                if (!_isBuffed)
                    _rangeAttack.SetDamage(_currentWeaponConfig.Damage);
                break;
            case WeaponType.Magic:
                break;
        }
    }

    public void Attack()
    {
        switch (_currentWeaponConfig.WeaponType)
        {
            case WeaponType.Melee:
                _attackArea.gameObject.SetActive(true);
                break;
            case WeaponType.Range:
                if (_arrowsAmount <= 0)
                    return;

                _rangeAttack.gameObject.SetActive(true);
                _rangeAttack.Shoot(_currentWeaponConfig.AttackSpeed);
                _arrowsAmount--;
                break;
            case WeaponType.Magic:
                break;
        }
    }

    public void Move(Vector2 vector)
    {
        _character.transform.position += new Vector3(vector.x, vector.y) * _moveSpeed * Time.deltaTime;
        _playerAnimationControl.SetAnimation(vector);
    }

    public void SetBuff(BuffConfig config)
    {
        StartCoroutine(StartBuffRoutine(config));
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

    public void Dash()
    {
        if (_rb == null) return;

        var dashDirection = GetMovementDirection();

        if (dashDirection == Vector2.zero)
        {
            dashDirection = GetFacingDirection();
        }

        _rb.velocity = Vector2.zero;
        _rb.AddForce(dashDirection * _dashForce, ForceMode2D.Impulse);

        _isDashing = true;
        _lastDashTime = Time.time;

        Invoke(nameof(ResetDash), 0.2f);
    }

    public void GetDiceDamage(int damage)
    {
        _health.Damage(damage);
    }

    private Vector2 GetMovementDirection()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        return new Vector2(horizontal, vertical).normalized;
    }

    private Vector2 GetFacingDirection()
    {
        return Vector2.right;
    }

    private void ResetDash()
    {
        _isDashing = false;
    }

    private IEnumerator StartBuffRoutine(BuffConfig config)
    {
        _isBuffed = true;

        switch (config.Type)
        {
            case BuffType.Damage:
                _rangeAttack.SetDamage(config.Increase);
                _attackArea.SetDamage(config.Increase);
                _light.color = config.Color;
                break;
            case BuffType.Health:
                _health.BuffHealth(config.Increase);
                _light.color = config.Color;
                break;
            case BuffType.Movement:
                _moveSpeed += config.Increase;
                _light.color = config.Color;
                break;
        }
        _light.intensity = 3;

        yield return new WaitForSeconds(config.Duration);

        _isBuffed = false;
        _light.color = Color.white;
        _light.intensity = 1;
        switch (config.Type)
        {
            case BuffType.Health:
                _health.ResetHealth();
                break;
            case BuffType.Movement:
                _moveSpeed -= config.Increase;
                break;
        }
    }
}
