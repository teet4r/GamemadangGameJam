using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IncrementData", menuName = "Scriptable Objects/IncrementData")]
public class IncrementData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private string _description;
    public string Description => _description;

    [SerializeField] private float[] _values;
    [SerializeField] private float[] _values2;

    private int _level = 0;

    public Action OnLevelUp;

    public bool IsMaxLevel => _level >= _values.Length;
    public float CurrentValue => _values[_level];
    public float CurrentValue2 => _values2[_level];
    public void LevelUp()
    {
        if (IsMaxLevel)
            return;

        ++_level;
    }
}
