using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTypeUi : MonoBehaviour
{
    [SerializeField] private Image _weapon;
    [SerializeField] private Sprite[] _sprites;
    private Character _character;

    private void Start()
    {
        _character = FindObjectOfType<Character>();
    }

    private void Update()
    {
        switch (_character.CurrentWeaponConfig.WeaponType)
        {
            case WeaponType.Melee:
                _weapon.sprite = _sprites[0];
                break;
            case WeaponType.Range:
                _weapon.sprite = _sprites[1];
                break;
        }
    }
}
