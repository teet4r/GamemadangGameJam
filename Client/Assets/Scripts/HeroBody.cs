using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class HeroBody : MonoBehaviour, ICollidable
{
    [Header("References")]
    [SerializeField] private SpinningSwordController _swordController;
    [SerializeField] private MercenaryController _mercenaryController;
    private Rigidbody2D _rigid;
    private Animator _animator;
    public SpinningSwordController swordController => _swordController;
    public MercenaryController mercenaryController => _mercenaryController;

    [Header("Stats")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _maxHp = 100;
    [SerializeField] private float _delaySecondsPerSkill;
    private float _delaySeconds = 0f;
    private bool _isAvoiding;
    private Coroutine _skillRoutine;

    private const string _HORIZONTAL = "Horizontal";
    private const string _VERTICAL = "Vertical";
    private readonly int _isRunHash = Animator.StringToHash("IsRun");

    public Vector2 Dir => (Vector2)transform.position - _prevPos;
    private Vector2 _prevPos = new();
    private Vector2 _vec = new();
    private Camera _mainCamera;

    public bool IsDead => _hp <= 0;
    public int Level => _level;
    private int _level;
    private int _hp;
    private int _exp;

    public bool IsRun => _vec != Vector2.zero;
    public bool IsFlip => transform.eulerAngles.y == 180;

    private void Awake()
    {
        TryGetComponent(out _rigid);
        TryGetComponent(out _animator);
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _prevPos = transform.position;
        _hp = _maxHp;
        _level = 1;
        _exp = 0;
        _isAvoiding = false;

        _swordController.Initialize();
        _swordController.StartSpin();
    }

    private void Update()
    {
        if (Ingame.Instance.IsGameEnd)
            return;

        _Move();
        _CameraFollowing();
        _Skill();
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
        if (Ingame.Instance.IsGameEnd)
            return;
        if (_isAvoiding)
            return;

        _hp -= damage;
        if (_hp < 0)
            _hp = 0;

        UIManager.Instance.Get<UIIngame>().UpdateHpBar(_hp, _maxHp);

        if (IsDead)
        {
            Ingame.Instance.IsGameEnd = true;
            UIManager.Instance.Show<UIGameoverPopup>();
            _swordController.StopSpin();
            Destroy(gameObject);
        }
    }

    public void GetHeal(int healAmount)
    {
        _hp += healAmount;
        if (_hp > _maxHp)
            _hp = _maxHp;

        UIManager.Instance.Get<UIIngame>().UpdateHpBar(_hp, _maxHp);
    }

    public void GetExp(int exp)
    {
        if (_level >= 30)
            return;

        _exp += exp;

        int neededExp = LevelData.ExpToLevelUp[_level];
        if (neededExp <= _exp)
        {
            _exp -= neededExp;
            ++_level;
            _maxHp += 200;
            GetHeal(_maxHp);

            if (_level % 3 == 0)
            {
                var result = IncrementTable.Instance.Pick3Increments();
                UIManager.Instance.Show<UILevelUpPopup>().Bind(result);
            }
        }
        UIManager.Instance.Get<UIIngame>().UpdateExpBar(_exp, LevelData.ExpToLevelUp[_level]);
        UIManager.Instance.Get<UIIngame>().UpdateLevelText(_level);
    }

    public void IncreaseMaxHp(int amount)
    {
        _maxHp += amount;
        GetHeal(_maxHp);
    }

    public void IncreaseSpeed(int amount)
    {
        _speed += amount;
    }

    public void DecreaseSkillCoolDown(int amount)
    {
        _delaySecondsPerSkill -= amount;
    }

    private void _Move()
    {
        _vec.x = Input.GetAxisRaw(_HORIZONTAL);
        _vec.y = Input.GetAxisRaw(_VERTICAL);
        _vec.Normalize();
        _rigid.MovePosition((Vector2)transform.position + Time.deltaTime * _speed * _vec);

        var eAngle = transform.eulerAngles;
        if (_vec.x < 0)
            eAngle.y = 180;
        else if (_vec.x > 0)
            eAngle.y = 0;
        transform.eulerAngles = eAngle;

        _animator.SetBool(_isRunHash, IsRun);

        _prevPos = transform.position;
    }

    private void _CameraFollowing()
    {
        var cameraPos = _mainCamera.transform.position;
        cameraPos.x = transform.position.x;
        cameraPos.y = transform.position.y;
        _mainCamera.transform.position = cameraPos;
    }

    private void _Skill()
    {
        _delaySeconds += Time.deltaTime;
        if (_delaySeconds < _delaySecondsPerSkill)
            return;

        if (_skillRoutine != null)
        {
            _delaySeconds = _delaySecondsPerSkill;
            return;
        }

        _delaySeconds -= _delaySecondsPerSkill;

        _skillRoutine = StartCoroutine(_SkillAvoidanceRoutine());
    }

    private IEnumerator _SkillAvoidanceRoutine()
    {
        var avoidanceEffect = ObjectPoolManager.Instance.Get<AvoidanceEffect>();
        avoidanceEffect.transform.SetParent(transform);
        avoidanceEffect.transform.position = transform.position;
        avoidanceEffect.Play();

        _isAvoiding = true;
        yield return YieldCache.WaitForSeconds(avoidanceEffect.PlayTime);
        _isAvoiding = false;

        _skillRoutine = null;
    }

    public void RecallHunter()
    {
        if (Addressables.InstantiateAsync("Hunter", transform.position, Quaternion.identity).WaitForCompletion().TryGetComponent(out Hunter hunter))
            _mercenaryController.AddMercenary(hunter);
    }

    public void RecallGuardian()
    {
        if (Addressables.InstantiateAsync("Guardian", transform.position, Quaternion.identity).WaitForCompletion().TryGetComponent(out Guardian guardian))
            _mercenaryController.AddMercenary(guardian);
    }

    public void RecallMagician()
    {
        if (Addressables.InstantiateAsync("Magician", transform.position, Quaternion.identity).WaitForCompletion().TryGetComponent(out Magician magician))
            _mercenaryController.AddMercenary(magician);
    }

    public void RecallHealer()
    {
        if (Addressables.InstantiateAsync("Healer", transform.position, Quaternion.identity).WaitForCompletion().TryGetComponent(out Healer healer))
            _mercenaryController.AddMercenary(healer);
    }
}
