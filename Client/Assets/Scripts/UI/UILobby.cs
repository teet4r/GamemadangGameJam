using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : UI
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _quitButton;

    protected override void Awake()
    {
        base.Awake();

        _startButton.SetListener(() =>
        {
            UIManager.Instance.Hide(this);
            AudioManager.Instance.Sfx.Play(SfxName.Click);
            SceneManager.Instance.LoadSceneAsync(SceneName._3_Ingame).Forget();
        });
        _howToPlayButton.SetListener(() =>
        {
            AudioManager.Instance.Sfx.Play(SfxName.Click);
            UIManager.Instance.Show<UIGameDescription>();
        });
        _quitButton.SetListener(() =>
        {
            AudioManager.Instance.Sfx.Play(SfxName.Click);
            Application.Quit();
        });
    }
}
