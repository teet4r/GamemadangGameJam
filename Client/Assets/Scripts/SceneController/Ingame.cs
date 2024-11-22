using UnityEngine;

public class Ingame : MonoBehaviour
{
    public static Ingame Instance => _instance;
    private static Ingame _instance;

    public Hero Hero => _hero;
    [SerializeField] private Hero _hero;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        UIManager.Instance.Show<UIIngame>().Bind();
    }
}
