using UnityEngine;

public class SkeletonWeapon : PoolObject, ICollidable
{
    public int Damage => _damage;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    Rigidbody2D _rigid;

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out _rigid);
    }

    public void Throw(Vector2 dir)
    {
        _rigid.linearVelocity = _speed * dir.normalized;
    }
}
