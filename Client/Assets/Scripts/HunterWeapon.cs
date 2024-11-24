using System.Collections;
using UnityEngine;

public class HunterWeapon : MercenaryThrowingWeapon
{
    [SerializeField] private float _flyTime;
    private Coroutine _timerRoutine;
    private int _additionalDamage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollidable collidable))
            return;

        if (collidable is Monster)
        {
            var monster = collidable as Monster;
            monster.GetDamage(damage + _additionalDamage);
            Return();
        }
    }

    public override void Throw(Vector2 dir)
    {
        if (_timerRoutine != null)
            return;

        rigid.SetRotation((Mathf.Atan2(dir.y, dir.x) * 180f / Mathf.PI));
        dir.Normalize();
        rigid.linearVelocity = speed * dir;

        _timerRoutine = StartCoroutine(_FlyTimer());
    }

    public void IncreaseDamage(int additionalDamage)
    {
        _additionalDamage += additionalDamage;
    }

    private IEnumerator _FlyTimer()
    {
        yield return YieldCache.WaitForSeconds(_flyTime);
        Return();
    }

    public override void Return()
    {
        if (_timerRoutine == null)
            return;

        StopCoroutine(_timerRoutine);
        _timerRoutine = null;
        _additionalDamage = 0;
        ObjectPoolManager.Instance.Return(this);
    }
}
