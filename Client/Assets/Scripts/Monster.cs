using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Collider2D))]
public abstract class Monster : PoolObject, ICollidable
{
    protected new Transform transform;
    protected Animator animator;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;

    public bool IsDead => hp <= 0;
    public int CollisionDamage => _collisionDamage;
    [SerializeField] private int _maxHp;
    [SerializeField] private float _speed;
    [SerializeField] private int _collisionDamage;

    protected int hp;
    protected virtual bool flipReverse => false;
    protected bool IsStopCondition => Ingame.Instance.IsGameEnd || IsDead || Ingame.Instance.Hero.IsNull() || Ingame.Instance.Hero.IsDead;

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out transform);
        TryGetComponent(out animator);
        TryGetComponent(out rigid);
        TryGetComponent(out spriteRenderer);
    }

    protected virtual void Update()
    {
        if (IsStopCondition)
        {
            rigid.linearVelocity = Vector2.zero;
            return;
        }

        _Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollidable collidable))
            return;

        if (collidable is SpinningSword)
        {
            var sword = collidable as SpinningSword;
            GetDamage(sword.Damage);
        }
    }

    public void Initialize()
    {
        hp = _maxHp;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        if (IsDead)
        {
            var effect = ObjectPoolManager.Instance.Get<MonsterDeadEffect>();
            effect.transform.position = rigid.position;
            effect.Play();

            UIManager.Instance.Get<UIIngame>().AddKillCount();
            AudioManager.Instance.Sfx.Play(SfxName.MonsterDie);

            if (Ingame.Instance.Hero.Level < 30 && Random.Range(0, 100) < 75)
            {
                var exp = ObjectPoolManager.Instance.Get<Experience>();
                exp.transform.position = rigid.position;
            }

            Return();
        }
    }

    private void _Move()
    {
        Vector2 HeroDir = Ingame.Instance.Hero.transform.position - transform.position;
        var nextPos = (Vector2)transform.position + (HeroDir.normalized * Time.deltaTime * _speed);
        rigid.MovePosition(nextPos);

        if (HeroDir.x > 0)
            spriteRenderer.flipX = !flipReverse;
        else if (HeroDir.x < 0)
            spriteRenderer.flipX = flipReverse;
    }
}
