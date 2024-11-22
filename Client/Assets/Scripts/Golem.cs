using UnityEngine;

public class Golem : Monster
{
    protected override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
