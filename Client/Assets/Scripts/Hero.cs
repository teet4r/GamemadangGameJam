using log4net.Core;
using UnityEngine;

public class Hero : MonoBehaviour, ICollidable
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpinningSwordController _swordController;
    public new Transform transform => _transform;
    private Transform _transform;

    [Header("Stats")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _maxHp = 100;

    private const string _HORIZONTAL = "Horizontal";
    private const string _VERTICAL = "Vertical";
    private readonly int _isRunHash = Animator.StringToHash("IsRun");

    public Vector2 Dir => (Vector2)_transform.position - _prevPos;
    private Vector2 _prevPos = new();
    private Vector2 _vec = new();
    private Camera _mainCamera;

    public bool IsDead => _hp <= 0;
    private int _level;
    private int _hp;
    private int _exp;

    private void Awake()
    {
        TryGetComponent(out _transform);
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _prevPos = _transform.position;
        _hp = _maxHp;
        _level = 1;
        _exp = 0;

        _swordController.Initialize();
        _swordController.StartSpin();
    }

    private void Update()
    {
        _Move();
        _CameraFollowing();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollidable collidable))
            return;

        if (collidable is MonsterThrowingWeapon)
        {
            var weapon = collidable as MonsterThrowingWeapon;
            GetDamage(weapon.Damage);
            weapon.Return();
        }
        else if (collidable is Experience)
        {
            var exp = collidable as Experience;
            GetExp(exp.Value);
            exp.Return();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out ICollidable collidable))
            return;

        if (collidable is Monster)
        {
            var monster = collidable as Monster;
            GetDamage(monster.CollisionDamage);
        }
    }

    public void GetDamage(int damage)
    {
        _hp -= damage;
        if (_hp < 0)
            _hp = 0;

        UIManager.Instance.Get<UIIngame>().UpdateHpBar(_hp, _maxHp);

        if (IsDead)
        {
            _swordController.StopSpin();
            Destroy(gameObject);
        }
    }

    public void GetExp(int exp)
    {
        _exp += exp;
    }

    private void _Move()
    {
        _vec.x = Input.GetAxisRaw(_HORIZONTAL);
        _vec.y = Input.GetAxisRaw(_VERTICAL);
        _vec.Normalize();
        _rigid.MovePosition((Vector2)_transform.position + Time.deltaTime * _speed * _vec);

        if (_vec.x < 0)
            _spriteRenderer.flipX = true;
        else if (_vec.x > 0)
            _spriteRenderer.flipX = false;

        _animator.SetBool(_isRunHash, _vec != Vector2.zero);

        _prevPos = _transform.position;
    }

    private void _CameraFollowing()
    {
        var cameraPos = _mainCamera.transform.position;
        cameraPos.x = _transform.position.x;
        cameraPos.y = _transform.position.y;
        _mainCamera.transform.position = cameraPos;
    }
}
