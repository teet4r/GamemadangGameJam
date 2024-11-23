using UnityEngine;

public class Golem : Monster
{
    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
