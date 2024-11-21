using UnityEngine;

public class Hero : PoolObject
{
    [Header("Reference")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private Animator _animator;

    [Header("Stat")]
    [SerializeField] private float _speed = 5f;

    private const string _HORIZONTAL = "Horizontal";
    private const string _VERTICAL = "Vertical";
    private readonly int _isRunHash = Animator.StringToHash("IsRun");

    private Vector2 _vec = new();

    private void Update()
    {
        _Move();
    }

    private void _Move()
    {
        _vec.x = Input.GetAxisRaw(_HORIZONTAL);
        _vec.y = Input.GetAxisRaw(_VERTICAL);
        _vec.Normalize();
        _rigid.linearVelocity = _vec * _speed;

        if (_vec.x < 0)
            _spriteRenderer.flipX = true;
        else if (_vec.x > 0)
            _spriteRenderer.flipX = false;

        _animator.SetBool(_isRunHash, _vec != Vector2.zero);
    }
}
