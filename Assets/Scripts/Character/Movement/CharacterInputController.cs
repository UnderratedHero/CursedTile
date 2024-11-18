using Assets.Scripts.Character;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField] private GameObject _controlableGameObject;
    [SerializeField] private List<InputMapping> inputMappings;

    private Dictionary<KeyCode, Vector2> keyToDirection;
    private IControllable _controlableCharacter;

    void Start()
    {
        _controlableCharacter = _controlableGameObject.GetComponent<IControllable>();

        if (_controlableCharacter == null)
            throw new NullReferenceException("IControllable is empty");

        keyToDirection = new Dictionary<KeyCode, Vector2>();
        foreach (var mapping in inputMappings)
        {
            if (!keyToDirection.ContainsKey(mapping.key))
                keyToDirection.Add(mapping.key, mapping.direction);
        }
    }

    void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        var direction = Vector2.zero;

        foreach (var key in keyToDirection.Keys)
        {
            if (Input.GetKey(key))
            {
                direction += keyToDirection[key];
            }
        }
        _controlableCharacter.Move(direction.normalized);
    }
}

[Serializable]
public class InputMapping
{
    public KeyCode key;          
    public Vector2 direction;   
}
