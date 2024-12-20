using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSwordController : MonoBehaviour
{
    [SerializeField] private HeroBody _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceFromTarget;
    [SerializeField] private int _defaultSwordCount;
    [SerializeField] private SpinningSword[] _swordPrefabs;

    private List<SpinningSword> _spinningSwords = new();
    private Coroutine _spinRoutine;

    private float _swordDegree;
    private int _swordLevel;
    private bool _spinDir;

    public void Initialize()
    {
        _swordDegree = 0f;
        _swordLevel = 0;
        _spinDir = true;
        AddSword(_defaultSwordCount);
    }

    public void StartSpin()
    {
        if (_spinRoutine != null)
            return;

        _spinRoutine = StartCoroutine(_StartSpin());
    }

    public void SwitchSpinDir()
    {
        _spinDir = !_spinDir;
    }

    private IEnumerator _StartSpin()
    {
        while (true)
        {
            for (int i = 0; i < _spinningSwords.Count; ++i)
            {
                var rad = Mathf.Deg2Rad * (_swordDegree + i * (360f / _spinningSwords.Count));
                var x = _distanceFromTarget * Mathf.Sin(rad);
                var y = _distanceFromTarget * Mathf.Cos(rad);
                var targetPos = _target.transform.position;
                targetPos.x += x;
                targetPos.y += y;
                _spinningSwords[i].transform.SetPositionAndRotation(targetPos, Quaternion.Euler(0f, 0f, (_swordDegree - 45f + i * (360f / _spinningSwords.Count)) * -1));
                if (_swordDegree > 360f)
                    _swordDegree %= 360f;
            }
            _swordDegree += Time.deltaTime * _speed * (_spinDir ? 1 : -1);

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
            Destroy(_spinningSwords[i]);
        _spinningSwords.Clear();
    }

    public void AddSword(int count)
    {
        for (int i = 0; i < count; ++i)
            _spinningSwords.Add(Instantiate(_swordPrefabs[_swordLevel], transform));
    }

    public void UpgradeSword()
    {
        ++_swordLevel;

        for (int i = 0; i < _spinningSwords.Count; ++i)
        {
            Destroy(_spinningSwords[i].gameObject);
            _spinningSwords[i] = Instantiate(_swordPrefabs[_swordLevel], transform);
        }
    }

    public void IncreaseSpeed(int amount)
    {
        _speed += amount;
    }
}
