using UnityEngine;

public class UILevelUpPopup : UI
{
    [SerializeField] private UILevelUpPopupSlot[] _slots;

    public void Bind()
    {
        Time.timeScale = 0f;

        for (int i = 0; i < _slots.Length; ++i)
            _slots[i].Bind(() =>
            {
                Time.timeScale = 1f;
                UIManager.Instance.Hide(this);
            },
            null,
            "",
            "");
    }
}
