using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Effect : PoolObject
{
    private Animator _animator;
    [SerializeField] private float _playTime;
    public float PlayTime => _playTime;

    private readonly int _playHash = Animator.StringToHash("Play");

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out _animator);
    }

    public void Play()
    {
        StartCoroutine(_Play());
    }

    private IEnumerator _Play()
    {
        _animator.SetTrigger(_playHash);
        yield return YieldCache.WaitForSeconds(_playTime);
        Return();
    }
}
