using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIGameoverPopup : UI
{
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _goMainButton;
    [SerializeField] private Button _quitButton;

    protected override void Awake()
    {
        base.Awake();

        _retryButton.SetListener(() =>
        {
            SceneManager.Instance.LoadSceneAsync(SceneName._3_Ingame).Forget();
            ObjectPoolManager.Instance.ClearAll();
        });

        _goMainButton.SetListener(() =>
        {
            SceneManager.Instance.LoadSceneAsync(SceneName._2_Lobby).Forget();
            ObjectPoolManager.Instance.ClearAll();
        });

        _quitButton.SetListener(() =>
        {
            Application.Quit();
        });
    }
}
