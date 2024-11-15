using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiOpenManage : MonoBehaviour
{
    [SerializeField] private List<ButtonCanvasPair> _openButtons;
    private Canvas _currentCanvas;

    private void Awake()
    {
        _currentCanvas = GetComponent<Canvas>();
        if (_currentCanvas is null)
            return;

        foreach (ButtonCanvasPair pair in _openButtons)
        {
            var button = pair.Button;
            var canvas = pair.Canvas;
            var isClosing = pair.IsClosing;

            button.onClick.AddListener(() => OnOpenButtonClick(canvas, isClosing));
        }
    }

    private void OnOpenButtonClick(GameObject openCanvas, bool isClosing)
    {
        if (isClosing)
            _currentCanvas.gameObject.SetActive(false);
        
        openCanvas.SetActive(true);
    }
}

[System.Serializable]
public class ButtonCanvasPair
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private bool _isClosing;

    public Button Button { get { return _button; } }
    public GameObject Canvas { get { return _canvas; } }
    public bool IsClosing { get { return _isClosing; } }
}