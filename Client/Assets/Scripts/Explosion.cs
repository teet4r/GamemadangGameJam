using UnityEngine;

public class Explosion : PoolObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;

    public void Attack()
    {
        var explosionEffect = ObjectPoolManager.Instance.Get<ExplosionEffect>();
        explosionEffect.transform.position = transform.position;
        explosionEffect.Play();

        var monsters = Physics2D.OverlapCircleAll(transform.position, _damageRadius, 1 << 7/*몬스터*/);
        for (int i = 0; i < monsters.Length; ++i)
        {
            if (!monsters[i].TryGetComponent(out Monster monster))
                return;

            monster.GetDamage(_damage);
        }

        Return();
    }

    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
