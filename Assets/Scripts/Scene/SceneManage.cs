using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField] private string _goToScene;
    [SerializeField] private EventHandler _eventHandler;
    private bool _canSwitchScene = true;

    private void OnEnable()
    {
        if (_eventHandler != null)
            _eventHandler.OnActionCalled += TryGoToScene;
    }

    private void OnDestroy()
    {
        if (_eventHandler != null)
            _eventHandler.OnActionCalled -= TryGoToScene;
    }

    public void TryGoToScene()
    {
        if (!_canSwitchScene)
            return;

        GoToScene();
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwitchState(bool state)
    {
        _canSwitchScene = state;
    }

    private void GoToScene()
    {
        SceneManager.LoadScene(_goToScene);
    }

}
