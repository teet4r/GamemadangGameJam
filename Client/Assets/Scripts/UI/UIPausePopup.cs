using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIPausePopup : UI
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _goMainButton;
    [SerializeField] private Button _quitButton;

    protected override void Awake()
    {
        base.Awake();

        _resumeButton.SetListener(() =>
        {
            Time.timeScale = 1f;
            AudioManager.Instance.Sfx.Play(SfxName.Click);
            UIManager.Instance.Hide(this);
        });

        _goMainButton.SetListener(() =>
        {
            Time.timeScale = 1f;
            AudioManager.Instance.Sfx.Play(SfxName.Click);
            SceneManager.Instance.LoadSceneAsync(SceneName._2_Lobby).Forget();
            ObjectPoolManager.Instance.ClearAll();
            UIManager.Instance.ClearAll();
        });

        _quitButton.SetListener(() =>
        {
            AudioManager.Instance.Sfx.Play(SfxName.Click);
            Application.Quit();
        });
    }

    public void Bind()
    {
        Time.timeScale = 0f;
    }
}
