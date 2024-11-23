using UnityEngine;

public class Snake : Monster
{
    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
