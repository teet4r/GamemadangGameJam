using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSwordController : MonoBehaviour
{
    [SerializeField] private Hero _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceFromTarget;
    [SerializeField] private int _defaultSwordCount;
    private float _degree;

    private List<SpinningSword> _spinningSwords = new();
    private Coroutine _spinRoutine;

    public void Initialize()
    {
        _degree = 0f;
        AddSword(_defaultSwordCount);
    }

    public void StartSpin()
    {
        if (_spinRoutine != null)
            return;

        _spinRoutine = StartCoroutine(_StartSpin());
    }

    private IEnumerator _StartSpin()
    {
        while (true)
        {
            for (int i = 0; i < _spinningSwords.Count; ++i)
            {
                var rad = Mathf.Deg2Rad * (_degree + i * (360f / _spinningSwords.Count));
                var x = _distanceFromTarget * Mathf.Sin(rad);
                var y = _distanceFromTarget * Mathf.Cos(rad);
                var targetPos = _target.transform.position;
                targetPos.x += x;
                targetPos.y += y;
                _spinningSwords[i].transform.SetPositionAndRotation(targetPos, Quaternion.Euler(0f, 0f, (_degree - 45f + i * (360f / _spinningSwords.Count)) * -1));
                if (_degree > 360f)
                    _degree %= 360f;
            }
            _degree += Time.deltaTime * _speed;

            yield return null;
        }
    }

    public void StopSpin()
    {
        if (_spinRoutine == null)
            return;

        StopCoroutine(_spinRoutine);
        _spinRoutine = null;

        for (int i = 0; i < _spinningSwords.Count; ++i)
            ObjectPoolManager.Instance.Return(_spinningSwords[i]);

        _spinningSwords.Clear();
    }

    public void AddSword(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            var sword = ObjectPoolManager.Instance.Get<SpinningSword>();
            _spinningSwords.Add(sword);
        }
    }
}
