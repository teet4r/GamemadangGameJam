using UnityEngine;
using UnityEngine.UI;

public class UIIngame : UI
{
    [Header("References")]
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _killCountText;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _expBar;
    [SerializeField] private Text _levelText;

    [Header("Variables")]
    [SerializeField] private float _totalTime;

    public bool IsGameover => _isGameover;
    private bool _isGameover;

    private int _killCount;

    private void Update()
    {
        _UpdateTimer();
    }

    public void Bind()
    {
        _killCount = 0;
        _isGameover = false;
        UpdateHpBar(1, 1);
        UpdateExpBar(1, 1);
        UpdateLevelText(1);
    }

    private void _UpdateTimer()
    {
        _totalTime -= Time.deltaTime;
        if (_totalTime < 0f)
        {
            _totalTime = 0f;
            if (!_isGameover)
                _isGameover = true;
        }

        int minutes = (int)_totalTime / 60;
        int seconds = (int)_totalTime % 60;

        _timerText.text = $"{minutes}:{seconds}";
    }

    public void AddKillCount()
    {
        if (_isGameover)
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
}
