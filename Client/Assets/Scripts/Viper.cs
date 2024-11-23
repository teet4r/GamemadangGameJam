using UnityEngine;

public class Viper : Monster
{
    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
