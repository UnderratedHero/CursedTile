using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInputManage : MonoBehaviour
{
    [SerializeField] private List<KeyCanvasPair> _openKeyNames;
    private void Update()
    {
        foreach (var key in _openKeyNames)
        {
            if (!Enum.TryParse(key.KeyName, out KeyCode keyCode) ||
                !Input.GetKeyDown(keyCode))
                continue;

            key.Canvas.SetActive(!key.Canvas.activeInHierarchy);
        }
    }
}

[System.Serializable]
public class KeyCanvasPair
{
    [SerializeField] private string _keyName;
    [SerializeField] private GameObject _canvas;

    public string KeyName { get { return _keyName; } }
    public GameObject Canvas { get { return _canvas; } }
}
