using UnityEngine;

public class Hunter : Mercenary
{
    [SerializeField] private float _delaySecondsPerShot;
    private float _delaySeconds = 0f;

    private readonly int _isRunHash = Animator.StringToHash("IsRun");

    protected override void Update()
    {
        base.Update();

        var hero = Ingame.Instance.Hero;
        if (hero.IsNull() || hero.IsDead)
            return;

        animator.SetBool(_isRunHash, hero.IsRun);
        _delaySeconds += Time.deltaTime;
        if (_delaySeconds < _delaySecondsPerShot)
            return;

        _delaySeconds -= _delaySecondsPerShot;

        var weapon = ObjectPoolManager.Instance.Get<HunterWeapon>();
        weapon.transform.position = transform.position;
        weapon.Throw(transform.position - hero.transform.position);
    }
}
