using UnityEngine;

public class Ingame : MonoBehaviour
{
    public static Ingame Instance => _instance;
    private static Ingame _instance;

    public HeroBody Hero => _hero;
    [SerializeField] private HeroBody _hero;

    public bool IsGameEnd;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        IsGameEnd = false;
        UIManager.Instance.Show<UIIngame>().Bind();
        AudioManager.Instance.Bgm.Play(BgmName.Ingame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UIManager.Instance.Show<UIPausePopup>().Bind();
    }
}
