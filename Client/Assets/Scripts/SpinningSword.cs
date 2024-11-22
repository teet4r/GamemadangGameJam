using UnityEngine;

public class SpinningSword : PoolObject, ICollidable
{
    public int Damage => _swordDatas[_level].Damage;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpinningSwordData[] _swordDatas;

    private int _level = 0;

    public void Upgrade()
    {
        if (_level >= _swordDatas.Length)
            return;

        ++_level;
        _spriteRenderer.sprite = _swordDatas[_level].Sprite;
    }
}
