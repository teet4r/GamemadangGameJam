using UnityEngine;

public class Snake : Monster
{
    protected override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
