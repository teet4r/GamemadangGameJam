using UnityEngine;

public class ShieldBreakingEffect : Effect
{
    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
