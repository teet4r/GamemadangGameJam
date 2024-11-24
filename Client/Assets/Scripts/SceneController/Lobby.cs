using UnityEngine;

public class Lobby : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.Show<UILobby>();
        AudioManager.Instance.Bgm.Play(BgmName.Lobby);
    }
}
