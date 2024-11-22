using Cysharp.Threading.Tasks;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    private void Start()
    {
        _BootStrap().Forget();
    }

    private async UniTask _BootStrap()
    {
        await UniTask.WaitUntil(() =>
           AudioManager.Instance.IsLoaded && ObjectPoolManager.Instance.IsLoaded &&
           SceneManager.Instance.IsLoaded && UIManager.Instance.IsLoaded
        );

        AudioManager.Instance.Bgm.Initialize();
        AudioManager.Instance.Sfx.Initialize();

        SceneManager.Instance.LoadSceneAsync(SceneName._2_Lobby).Forget();
    }
}
