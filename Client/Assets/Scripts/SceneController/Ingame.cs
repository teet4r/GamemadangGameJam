using UnityEngine;

public class Ingame : MonoBehaviour
{
    public static Ingame Instance => _instance;
    private static Ingame _instance;

    public HeroBody Hero => _hero;
    [SerializeField] private HeroBody _hero;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        UIManager.Instance.Show<UIIngame>().Bind();
    }
}
