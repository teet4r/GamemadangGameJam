using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIIngame : UI
{
    [Header("References")]
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _killCountText;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _expBar;
    [SerializeField] private Text _levelText;
    [SerializeField] private Image _coolDownImage;
    [SerializeField] private UIMercenarySlot[] _mercenarySlots;

    [Header("Variables")]
    [SerializeField] private float _totalTime;

    private int _killCount;

    private void Update()
    {
        _UpdateTimer();
    }

    public void Bind()
    {
        _killCount = 0;
        UpdateHpBar(1, 1);
        UpdateExpBar(0, 1);
        UpdateLevelText(1);
        //for (int i = 0; i < _mercenarySlots.Length; ++i)
        //    _mercenarySlots[i].Bind(null, null);
    }

    private void _UpdateTimer()
    {
        _totalTime -= Time.deltaTime;
        if (_totalTime < 0f)
        {
            _totalTime = 0f;
            if (!Ingame.Instance.IsGameEnd)
            {
                Ingame.Instance.IsGameEnd = true;
                UIManager.Instance.Show<UIClearPopup>().Bind();
            }
        }

        int minutes = (int)_totalTime / 60;
        int seconds = (int)_totalTime % 60;

        _timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void AddKillCount()
    {
        if (Ingame.Instance.IsGameEnd)
            return;

        _killCountText.text = $"킬 횟수: {++_killCount}";
    }

    public void UpdateHpBar(int curHp, int maxHp)
    {
        _hpBar.fillAmount = (float)curHp / maxHp;
    }

    public void UpdateExpBar(int curExp, int maxExp)
    {
        _expBar.fillAmount = (float)curExp / maxExp;
    }

    public void UpdateLevelText(int curLevel)
    {
        _levelText.text = $"Lv. {curLevel}";
    }

    public void UpdateSkillCoolDown(float remainCool, float totalCool)
    {
        _coolDownImage.fillAmount = remainCool / totalCool;
    }
}
