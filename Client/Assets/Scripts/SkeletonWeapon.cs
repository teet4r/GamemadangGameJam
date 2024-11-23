using System.Collections;
using UnityEngine;

public class SkeletonWeapon : MonsterThrowingWeapon
{
    [SerializeField] private float _flyTime;
    private Coroutine _timerRoutine;

    public override void Throw(Vector2 dir)
    {
        if (_timerRoutine != null)
            return;

        rigid.SetRotation((Mathf.Atan2(dir.y, dir.x) * 180f / Mathf.PI) + 90f);
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
