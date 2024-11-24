using UnityEngine;

public class Explosion : PoolObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;
    private int _additionalDamage = 0;

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

            monster.GetDamage(_damage + _additionalDamage);
        }

        Return();
    }

    public void IncreaseDamage(int amount)
    {
        _additionalDamage += amount;
    }

    public override void Return()
    {
        _additionalDamage = 0;
        ObjectPoolManager.Instance.Return(this);
    }
}
