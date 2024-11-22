using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private EventHandler _handler;
    private bool _isPaused = false;

    private void OnEnable()
    {
        _handler.OnActionCalled += TogglePause;
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        _handler.OnActionCalled -= TogglePause;
    }
}
