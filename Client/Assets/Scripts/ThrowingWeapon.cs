using UnityEngine;

public abstract class ThrowingWeapon : PoolObject, ICollidable
{
    public int Damage => damage;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    protected Rigidbody2D rigid;

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out rigid);
    }

    public abstract void Throw(Vector2 dir);
}
