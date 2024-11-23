using UnityEngine;

public class Guardian : Mercenary
{
    [SerializeField] private float _delaySecondsPerSkill;
    private float _delaySeconds = 0f;

    private readonly int _isRunHash = Animator.StringToHash("IsRun");
    private readonly int _shieldHash = Animator.StringToHash("Shield");

    protected override void Update()
    {
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
        shield.StartDefence();
    }
}
