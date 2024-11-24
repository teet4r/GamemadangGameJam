using UnityEngine;

public class Magician : Mercenary
{
    [SerializeField] private float _delaySecondsPerSkill;
    [SerializeField] private float _monsterDetectionRadius;
    private float _delaySeconds = 0f;
    private int _additionalDamage = 0;

    private readonly int _isRunHash = Animator.StringToHash("IsRun");
    private readonly int _explosionHash = Animator.StringToHash("Explosion");

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

        var monster = Physics2D.OverlapCircle(transform.position, _monsterDetectionRadius, 1 << 7/*몬스터*/);
        if (monster.IsNull())
        {
            _delaySeconds = _delaySecondsPerSkill;
            return;
        }
        
        _delaySeconds -= _delaySecondsPerSkill;
        
        animator.SetTrigger(_explosionHash);
        var explosion = ObjectPoolManager.Instance.Get<Explosion>();
        explosion.transform.position = monster.transform.position;
        explosion.IncreaseDamage(_additionalDamage);
        explosion.Attack();
    }

    public void LevelUp(float decreaseCoolDownAmount)
    {
        _delaySecondsPerSkill -= decreaseCoolDownAmount;
    }

    public void IncreaseDamage(int amount)
    {
        _additionalDamage += amount;
    }
}
