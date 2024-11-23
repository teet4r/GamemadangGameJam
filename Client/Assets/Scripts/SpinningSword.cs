using UnityEngine;

public class SpinningSword : MonoBehaviour, ICollidable
{
    public int Damage => damage;
    [SerializeField] protected int damage;
}
