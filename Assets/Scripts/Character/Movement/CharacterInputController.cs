using Assets.Scripts.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField] public GameObject controlableGameObject;
    private IControllable controlableCharacter;

    private GameInput gameInput;

    // Start is called before the first frame update
    void Start()
    {
        controlableCharacter = controlableGameObject.GetComponent<IControllable>();

        if (controlableCharacter == null)
            throw new NullReferenceException("IControllable is empty");
    }

    private void Awake()
    {
        gameInput = new GameInput();
        gameInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        var direction = gameInput.Gameplay.Movement.ReadValue<Vector2>();

        controlableCharacter.Move(direction);
    }
}
