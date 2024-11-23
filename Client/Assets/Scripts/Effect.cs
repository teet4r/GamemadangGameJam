using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Effect : PoolObject
{
    private Animator _animator;
    [SerializeField] private int _playTimeMilliSeconds;

    private readonly int _playHash = Animator.StringToHash("Play");

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out _animator);
    }

    public void Play()
    {
        _Play().Forget();
    }

    private async UniTask _Play()
    {
        _animator.SetTrigger(_playHash);
        await UniTask.Delay(_playTimeMilliSeconds);
        Return();
    }
}
