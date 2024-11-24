using UnityEngine;
using UnityEngine.UI;

public class UIGameDescription : UI
{
    [SerializeField] private Button _exit;

    protected override void Awake()
    {
        base.Awake();

        _exit.SetListener(() => UIManager.Instance.Hide(this));
    }
}
