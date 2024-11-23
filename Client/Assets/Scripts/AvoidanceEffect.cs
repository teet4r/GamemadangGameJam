using UnityEngine;

public class AvoidanceEffect : Effect
{
    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
