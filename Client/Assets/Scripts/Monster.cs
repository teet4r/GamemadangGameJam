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
    [SerializeField] private int _maxHp;
    [SerializeField] private float _speed;

    protected int hp;

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
        if (IsDead || Ingame.Instance.Hero.IsDead)
            return;

        _Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollidable collidable))
        {
            if (collidable is SpinningSword)
            {
                var sword = collidable as SpinningSword;
                GetDamage(sword.Damage);
            }
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

            Return();
        }
    }

    private void _Move()
    {
        Vector2 HeroDir = Ingame.Instance.Hero.transform.position - transform.position;
        var nextPos = (Vector2)transform.position + (HeroDir.normalized * Time.deltaTime * _speed);
        rigid.MovePosition(nextPos);

        if (HeroDir.x > 0)
            spriteRenderer.flipX = true;
        else if (HeroDir.x < 0)
            spriteRenderer.flipX = false;
    }

    protected abstract void Return();
}
