using UnityEngine;

public class Hero : PoolObject, ICollidable
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpinningSwordController _swordController;

    [Header("Stats")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _maxHp = 100;

    private const string _HORIZONTAL = "Horizontal";
    private const string _VERTICAL = "Vertical";
    private readonly int _isRunHash = Animator.StringToHash("IsRun");

    public Vector2 Dir => (Vector2)transform.position - _prevPos;
    private Vector2 _prevPos = new();
    private Vector2 _vec = new();
    private Camera _mainCamera;

    public bool IsDead => _hp <= 0;
    private int _hp;

    private void Start()
    {
        _mainCamera = Camera.main;
        _prevPos = transform.position;
        _hp = _maxHp;

        _swordController.Initialize();
        _swordController.StartSpin();
    }

    private void Update()
    {
        _Move();
        _CameraFollowing();
    }

    public void GetDamage(int damage)
    {
        _hp -= damage;
        if (IsDead)
            _swordController.StopSpin();
    }

    private void _Move()
    {
        _vec.x = Input.GetAxisRaw(_HORIZONTAL);
        _vec.y = Input.GetAxisRaw(_VERTICAL);
        _vec.Normalize();
        _rigid.MovePosition((Vector2)transform.position + Time.deltaTime * _speed * _vec);

        if (_vec.x < 0)
            _spriteRenderer.flipX = true;
        else if (_vec.x > 0)
            _spriteRenderer.flipX = false;

        _animator.SetBool(_isRunHash, _vec != Vector2.zero);

        _prevPos = transform.position;
    }

    private void _CameraFollowing()
    {
        var cameraPos = _mainCamera.transform.position;
        cameraPos.x = transform.position.x;
        cameraPos.y = transform.position.y;
        _mainCamera.transform.position = cameraPos;
    }
}
