using UnityEngine;

public class MonsterDeadEffect : Effect
{
    protected override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
