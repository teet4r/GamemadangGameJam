using UnityEngine;

public class Mercenary : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
    }

    protected virtual void Update()
    {
        var hero = Ingame.Instance.Hero;
        if (hero.IsNull() || hero.IsDead)
            return;

        spriteRenderer.flipX = hero.IsFlip;
    }
}
