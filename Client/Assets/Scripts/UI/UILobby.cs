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
            SceneManager.Instance.LoadSceneAsync(SceneName._3_Ingame).Forget();
        });
        _howToPlayButton.SetListener(() => UIManager.Instance.Show<UIGameDescription>());
        _quitButton.SetListener(() => Application.Quit());
    }
}
