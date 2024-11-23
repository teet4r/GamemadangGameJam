using UnityEngine;

public class ExplosionEffect : Effect
{
    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
