using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public TextMeshProUGUI LoadingPercentage;
    public Image LoadingProgressBar;

    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false;
    private static bool isSceneLoading = false;

    private Animator componentAnimator;
    private AsyncOperation loadingSceneOperation;

    public static void SwitchToScene(string sceneName)
    {
        if (isSceneLoading || instance == null || instance.componentAnimator == null)
            return;

        isSceneLoading = true;

        instance.componentAnimator.SetTrigger("sceneClosing");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadingSceneOperation.allowSceneActivation = false;

        instance.LoadingProgressBar.fillAmount = 0;
    }

    private void Start()
    {
        instance = this;

        componentAnimator = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation)
        {
            componentAnimator.SetTrigger("sceneOpening");
            LoadingProgressBar.fillAmount = 1;

            shouldPlayOpeningAnimation = false;
            isSceneLoading = false;
        }
    }

    private void Update()
    {
        if (loadingSceneOperation == null)
            return;

        LoadingPercentage.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";

        LoadingProgressBar.fillAmount = Mathf.Lerp(
            LoadingProgressBar.fillAmount,
            loadingSceneOperation.progress,
            Time.deltaTime * 5);
    }

    public void OnAnimationOver()
    {
        shouldPlayOpeningAnimation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}
