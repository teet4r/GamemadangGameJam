using System.Collections;
using UnityEngine;

public class Skeleton : Monster
{
    [SerializeField] private float _throwCoolDown;
    private float _time;

    protected override void Update()
    {
        base.Update();

        if (IsStopCondition)
            return;

        _Attack();
    }

    private void _Attack()
    {
        _time += Time.deltaTime;
        if (_time > _throwCoolDown)
        {
            _time -= _throwCoolDown;

            var weapon = ObjectPoolManager.Instance.Get<SkeletonWeapon>();
            weapon.transform.position = transform.position;
            Vector2 HeroDir = Ingame.Instance.Hero.transform.position - transform.position;
            weapon.Throw(HeroDir);
        }
    }

    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
