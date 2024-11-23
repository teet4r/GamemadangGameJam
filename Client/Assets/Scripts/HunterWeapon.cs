using System.Collections;
using UnityEngine;

public class HunterWeapon : MercenaryThrowingWeapon
{
    [SerializeField] private float _flyTime;
    private Coroutine _timerRoutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollidable collidable))
            return;

        if (collidable is Monster)
        {
            var monster = collidable as Monster;
            monster.GetDamage(damage);
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
        ObjectPoolManager.Instance.Return(this);
    }
}
