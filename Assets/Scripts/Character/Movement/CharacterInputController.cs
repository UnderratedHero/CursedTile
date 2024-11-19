using Assets.Scripts.Character;
using System;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField] private GameObject _controlableGameObject;
    private IControllable _controlableCharacter;
    private GameInput _gameInput;

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
    }

    private void ReadMovement()
    {
        var direction = _gameInput.Gameplay.Movement.ReadValue<Vector2>();

        _controlableCharacter.Move(direction.normalized);
    }
}
