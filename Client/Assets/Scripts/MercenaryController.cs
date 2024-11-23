using System.Collections.Generic;
using UnityEngine;

public class MercenaryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HeroBody _target;

    [Header("Variables")]
    [SerializeField] private float _distanceFromTarget;

    private const string _MOUSE_SCROLLWHEEL = "Mouse ScrollWheel";

    private Mercenary[] _mercenaries = new Mercenary[4];
    private int _degree = 0;

    private void Update()
    {
        if (_target.IsNull() || _target.IsDead)
            return;

        var value = Input.GetAxis(_MOUSE_SCROLLWHEEL);
        if (value > 0f) // 위로 올릴 때
        {
            // 반시계 방향으로 회전
            _degree -= 30;
            if (_degree < 0)
                _degree += 360;
        }
        else if (value < 0f) // 아래로 내릴 때
        {
            // 시계 방향으로 회전
            _degree += 30;
            if (_degree > 360)
                _degree -= 360;
        }

        for (int i = 0; i < _mercenaries.Length; ++i)
        {
            if (_mercenaries[i].IsNull())
                continue;

            var rad = Mathf.Deg2Rad * (_degree + i * (360f / _mercenaries.Length));
            var x = _distanceFromTarget * Mathf.Sin(rad);
            var y = _distanceFromTarget * Mathf.Cos(rad);
            var targetPos = _target.transform.position;
            targetPos.x += x;
            targetPos.y += y;
            _mercenaries[i].transform.position = targetPos;
        }
    }

    public void AddMercenary(Mercenary mercenary)
    {
        for (int i = 0; i < _mercenaries.Length; ++i)
            if (_mercenaries[i].IsNull())
            {
                mercenary.transform.SetParent(transform);
                _mercenaries[i] = mercenary;
                break;
            }
    }
}
