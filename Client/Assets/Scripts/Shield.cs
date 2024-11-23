using System.Collections;
using UnityEngine;

public class Shield : PoolObject, ICollidable
{
    [SerializeField] private int _maxHp;
    [SerializeField] private float _duration;
    private Animator _animator;

    private readonly int _defenceHash = Animator.StringToHash("Defence");
    private Coroutine _defenceRoutine;

    private int _hp;

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out _animator);
    }

    private void Update()
    {
        var hero = Ingame.Instance.Hero;
        if (hero.IsNull() || hero.IsDead)
        {
            _Destroy();
            return;
        }

        transform.position = hero.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollidable collidable))
            return;

        if (collidable is MonsterThrowingWeapon)
        {
            var weapon = collidable as MonsterThrowingWeapon;
            GetDamage(weapon.Damage);
            weapon.Return();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollidable collidable))
            return;

        if (collidable is Monster)
        {
            var monster = collidable as Monster;
            GetDamage(monster.CollisionDamage);
        }
    }

    public void StartDefence()
    {
        if (_defenceRoutine != null)
            return;

        _hp = _maxHp;
        _defenceRoutine = StartCoroutine(_DefenceRoutine());
    }

    private IEnumerator _DefenceRoutine()
    {
        _animator.SetTrigger(_defenceHash);
        yield return YieldCache.WaitForSeconds(_duration);
        _Destroy();
    }

    public void GetDamage(int damage)
    {
        if (_defenceRoutine == null)
            return;

        _hp -= damage;
        if (_hp <= 0)
            _Destroy();
    }

    private void _Destroy()
    {
        if (_defenceRoutine == null)
            return;

        StopCoroutine(_defenceRoutine);
        _defenceRoutine = null;

        var breakingEffect = ObjectPoolManager.Instance.Get<ShieldBreakingEffect>();
        breakingEffect.transform.position = transform.position;
        breakingEffect.Play();

        Return();
    }

    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
