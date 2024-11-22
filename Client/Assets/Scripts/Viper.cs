using UnityEngine;

public class Viper : Monster
{
    protected override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
