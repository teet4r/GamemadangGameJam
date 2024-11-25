using UnityEngine;
using UnityEngine.UI;

public class Healer : Mercenary
{
    [SerializeField] private int _healAmount;
    [SerializeField] private float _delaySecondsPerSkill;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _coolDownImage;
    private float _delaySeconds = 0f;

    private readonly int _isRunHash = Animator.StringToHash("IsRun");
    private readonly int _healHash = Animator.StringToHash("Heal");

    protected override void Update()
    {
        if (Ingame.Instance.IsGameEnd)
            return;

        base.Update();

        var hero = Ingame.Instance.Hero;
        if (hero.IsNull() || hero.IsDead)
            return;

        animator.SetBool(_isRunHash, hero.IsRun);

        _delaySeconds += Time.deltaTime;
        _coolDownImage.fillAmount = (_delaySecondsPerSkill - _delaySeconds) / _delaySecondsPerSkill;
        _canvasGroup.alpha = Ingame.Instance.showMercenariesCoolDown ? 1 : 0;

        if (_delaySeconds < _delaySecondsPerSkill)
            return;

        _delaySeconds -= _delaySecondsPerSkill;

        animator.SetTrigger(_healHash);
        hero.GetHeal(_healAmount);
        var healEffect = ObjectPoolManager.Instance.Get<HealEffect>();
        healEffect.transform.SetParent(hero.transform);
        healEffect.transform.position = hero.transform.position;
        healEffect.Play();
    }

    public void LevelUp(int amount)
    {
        _healAmount += amount;
    }

    public void IncreaseHealAmount(int amount)
    {
        _healAmount += amount;
    }

    public void DecreaseCoolDown(int amount)
    {
        _delaySecondsPerSkill -= amount;
    }
}
