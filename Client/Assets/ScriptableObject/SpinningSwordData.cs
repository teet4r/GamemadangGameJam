using UnityEngine;

[CreateAssetMenu(fileName = "SpinningSwordData", menuName = "Scriptable Objects/SpinningSwordData")]
public class SpinningSwordData : ScriptableObject
{
    public int Damage => _damage;
    [SerializeField] private int _damage;

    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
}
