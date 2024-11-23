using UnityEngine;

public class Experience : PoolObject, ICollidable
{
    public int Value => _value;
    [SerializeField] private int _value;

    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
