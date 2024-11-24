using UnityEngine;

public class Experience : PoolObject, ICollidable
{
    public int Value => _value;
    [SerializeField] private int _value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollidable collidable))
            return;

        if (collision.CompareTag("HeroBody"))
        {
            var hero = collidable as HeroBody;
            hero.GetExp(Value);
            Return();
        }
    }

    public override void Return()
    {
        ObjectPoolManager.Instance.Return(this);
    }
}
