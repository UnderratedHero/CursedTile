using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField] private string _goToScene;
    [SerializeField] private EventHandler _eventHandler;

    private void Start()
    {
        if (_eventHandler != null)
            _eventHandler.OnActionCalled += GoToScene;
    }

    public void GoToScene()
    {
        SceneManager.LoadScene(_goToScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        if (_eventHandler != null)
            _eventHandler.OnActionCalled -= GoToScene;
    }
}
