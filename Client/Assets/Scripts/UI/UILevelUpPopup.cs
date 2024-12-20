using UnityEngine;

public class UILevelUpPopup : UI
{
    [SerializeField] private UILevelUpPopupSlot[] _slots;

    public void Bind(IncrementData[] datas)
    {
        Time.timeScale = 0f;

        for (int i = 0; i < _slots.Length; ++i)
        {
            var ii = i;
            _slots[i].Bind(() =>
            {
                datas[ii].OnLevelUp();
                Time.timeScale = 1f;
                UIManager.Instance.Hide(this);
            },
            datas[i].Icon,
            datas[i].Name,
            datas[i].Description);
        }
    }
}
