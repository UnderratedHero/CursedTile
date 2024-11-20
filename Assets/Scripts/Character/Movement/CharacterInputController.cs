using Assets.Scripts.Character;
using System;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField] private GameObject _controlableGameObject;
    private IControllable _controlableCharacter;
    private GameInput _gameInput;

    private bool _isAttacking = false;
    private float _attackTime = 0.25f;
    private float _timer = 0f;


    void Start()
    {
        _controlableCharacter = _controlableGameObject.GetComponent<IControllable>();

        if (_controlableCharacter == null)
            throw new NullReferenceException("IControllable is empty");

        _gameInput = new GameInput();
        _gameInput.Enable();
    }

    void Update()
    {
        ReadMovement();
        ReadAttack();
        SwitchWeaponRead();
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
            _isAttacking = true;
            _controlableCharacter.Attack();
        }

        if (!_isAttacking)
            return;

        _timer += Time.deltaTime;

        if (_timer > _attackTime)
        {
            _isAttacking = false;
            _timer = 0f;
            _controlableCharacter.Unack();
        }
    }

    private void SwitchWeaponRead()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            _controlableCharacter.SwitchWeapon();
    }
}
