using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : UI
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    protected override void Awake()
    {
        base.Awake();

        _startButton.onClick.AddListener(() =>
        {
            UIManager.Instance.Hide(this);
            SceneManager.Instance.LoadSceneAsync(SceneName._3_Ingame).Forget();
        });
        _quitButton.onClick.AddListener(() => Application.Quit());
    }
}
