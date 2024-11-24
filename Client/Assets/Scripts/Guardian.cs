using UnityEngine;

public class Guardian : Mercenary
{
    [SerializeField] private float _delaySecondsPerSkill;
    private float _delaySeconds = 0f;
    private int _additionalShieldHp = 0;
    private int _additionalDuration = 0;

    private readonly int _isRunHash = Animator.StringToHash("IsRun");
    private readonly int _shieldHash = Animator.StringToHash("Shield");

    protected override void Update()
    {
        if (Ingame.Instance.IsGameEnd)
            return;

        base.Update();

        var hero = Ingame.Instance.Hero;
        if (hero.IsNull() || hero.IsDead)
            return;

        animator.SetBool(_isRunHash, hero.IsRun);
        _delaySeconds += Time.deltaTime;
        if (_delaySeconds < _delaySecondsPerSkill)
            return;

        _delaySeconds -= _delaySecondsPerSkill;

        animator.SetTrigger(_shieldHash);
        var shield = ObjectPoolManager.Instance.Get<Shield>();
        shield.IncreaseMaxHp(_additionalShieldHp);
        shield.IncreaseDuration(_additionalDuration);
        shield.StartDefence();
    }

    public void LevelUp(int increaseShieldHp)
    {
        _additionalShieldHp += increaseShieldHp;
    }

    public void IncreaseDuration(int amount)
    {
        _additionalDuration += amount;
    }

    public void DecreaseCoolDown(int amount)
    {
        _delaySecondsPerSkill -= amount;
    }
}
