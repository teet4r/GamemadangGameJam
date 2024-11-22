using UnityEngine;
using UnityEngine.UI;

public class UIIngame : UI
{
    [Header("References")]
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _killCountText;

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
}
