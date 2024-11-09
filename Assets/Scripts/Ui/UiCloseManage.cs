using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCloseManage : MonoBehaviour
{
    [SerializeField] private Canvas _goBackCanvas;
    [SerializeField] private Button _closeButton;

    private Canvas _currentCanvas;

    private void Awake()
    {
        _currentCanvas = GetComponent<Canvas>();

        if (_closeButton is null ||
            _goBackCanvas is null ||
            _currentCanvas is null)
            return;

        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        _currentCanvas.gameObject.SetActive(false);
        _goBackCanvas.gameObject.SetActive(true);
    }
}

