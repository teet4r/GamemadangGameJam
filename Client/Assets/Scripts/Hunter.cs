using UnityEngine;

public class Hunter : Mercenary
{
    [SerializeField] private float _delaySecondsPerShot;
    [SerializeField] private int _shotCountPerShot;
    private float _delaySeconds = 0f;
    private int _additionalDamage = 0;

    private readonly int _isRunHash = Animator.StringToHash("IsRun");

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
        if (_delaySeconds < _delaySecondsPerShot)
            return;

        _delaySeconds -= _delaySecondsPerShot;

        for (int i = 0; i < _shotCountPerShot; ++i)
        {
            var weapon = ObjectPoolManager.Instance.Get<HunterWeapon>();
            weapon.transform.position = transform.position;
            weapon.IncreaseDamage(_additionalDamage);
            weapon.Throw(transform.position - hero.transform.position);
        }
    }

    public void LevelUp(float decreaseCoolDownAmount)
    {
        _delaySecondsPerShot -= decreaseCoolDownAmount;
    }

    public void IncreaseShotCount(int amount)
    {
        _shotCountPerShot += amount;
    }

    public void IncreaseDamage(int additionalDamage)
    {
        _additionalDamage += additionalDamage;
    }
}
