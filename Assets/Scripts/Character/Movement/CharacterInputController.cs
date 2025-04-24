using Assets.Scripts.Character;
using System;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField] private GameObject _controlableGameObject;
    [SerializeField] private float _attackTime = 0.25f;
    
    private IControllable _controlableCharacter;
    private GameInput _gameInput;

    private bool _isAttacking = false;
    private float _timer = 0f;


    void Start()
    {
        try
        {
            _controlableCharacter = _controlableGameObject.GetComponent<IControllable>();

            if (_controlableCharacter == null)
                throw new NullReferenceException("IControllable is empty");

            _gameInput = new GameInput();
            _gameInput.Enable();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }

    void Update()
    {
        ReadMovement();
        ReadAttack();
        SwitchWeaponRead();
        DashRead();
    }

    private void ReadMovement()
    {
        var direction = _gameInput.Gameplay.Movement.ReadValue<Vector2>();

        _controlableCharacter.Move(direction.normalized);
    }

    private void ReadAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartAttack();
        }

        if (!_isAttacking)
            return;

        _timer += Time.deltaTime;

        if (_timer > _attackTime)
        {
            EndAttack();
        }
    }

    private void StartAttack()
    {
        if (_isAttacking)
            return;

        _isAttacking = true;
        _timer = 0f;
        _controlableCharacter.Attack();
    }

    private void EndAttack()
    {
        _isAttacking = false;
        _timer = 0f;
        _controlableCharacter.Unack();
    }

    private void SwitchWeaponRead()
    {
        if (_isAttacking)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
            _controlableCharacter.SwitchWeapon();
    }

    private void DashRead()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_controlableCharacter.IsDashing &&
            Time.time - _controlableCharacter.LastDashTime > _controlableCharacter.DashCooldown)
            _controlableCharacter.Dash();
    }
}
