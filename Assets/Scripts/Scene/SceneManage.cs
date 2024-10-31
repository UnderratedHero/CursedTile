using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField] private string _goToScene;

    public void GoToScene()
    {
        SceneManager.LoadScene(_goToScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
