using UnityEngine;

public class MonsterDeadEffect : Effect
{
    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
