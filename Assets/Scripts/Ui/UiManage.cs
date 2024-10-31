using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManage : MonoBehaviour
{
    [SerializeField] private Canvas _goBackCanvas;
    [SerializeField] private Button _closeButton;
    [SerializeField] private List<ButtonCanvasPair> _openButtons;
    private Canvas _currentCanvas;

    private void Awake()
    {
        _currentCanvas = GetComponent<Canvas>();    
        
        foreach (ButtonCanvasPair pair in _openButtons)
        {
            var button = pair.Button;
            var canvas = pair.Canvas;

            button.onClick.AddListener(() => OnOpenButtonClick(canvas));
        }
        
        if (_closeButton is null ||
            _currentCanvas is null || 
            _goBackCanvas is null)
        {
            return;
        }    

        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        _currentCanvas.gameObject.SetActive(false);
        _goBackCanvas.gameObject.SetActive(true);
    }

    private void OnOpenButtonClick(GameObject openCanvas)
    {
        _currentCanvas.gameObject.SetActive(false);
        openCanvas.SetActive(true);
    }
}

[System.Serializable]
public class ButtonCanvasPair
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _canvas;

    public Button Button { get { return _button; } }
    public GameObject Canvas { get { return _canvas; } }
}

