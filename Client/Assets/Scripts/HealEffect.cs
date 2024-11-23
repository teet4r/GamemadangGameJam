using UnityEngine;

public class HealEffect : Effect
{
    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
