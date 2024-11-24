using System.Collections.Generic;
using UnityEngine;

public class IncrementTable : MonoBehaviour
{
    public static IncrementTable Instance => _instance;
    private static IncrementTable _instance;

    [SerializeField] private IncrementData[] _incrementDatas;
    private List<int> _pickableIncrementIndexes = new();

    private void Awake()
    {
        _instance = this;

        for (int i = 0; i <= 9; ++i)
            _pickableIncrementIndexes.Add(i);

        Shuffle();
        _SetLevelUpAction();
    }

    public IncrementData[] Pick3Increments()
    {
        IncrementData[] result = new IncrementData[3];

        for (int i = 0; i < 3; ++i)
            result[i] = _incrementDatas[_pickableIncrementIndexes[i]];

        return result;
    }

    public void Shuffle()
    {
        for (int i = 0; i < 50; ++i)
        {
            int idx1 = _pickableIncrementIndexes[Random.Range(0, _pickableIncrementIndexes.Count)];
            int idx2 = _pickableIncrementIndexes[Random.Range(0, _pickableIncrementIndexes.Count)];

            int t = _pickableIncrementIndexes[idx1];
            _pickableIncrementIndexes[idx1] = _pickableIncrementIndexes[idx2];
            _pickableIncrementIndexes[idx2] = t;
        }
    }

    private void _SetLevelUpAction()
    {
        _incrementDatas[0].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.IncreaseMaxHp((int)_incrementDatas[0].CurrentValue);
            _incrementDatas[0].LevelUp();
            if (_incrementDatas[0].IsMaxLevel)
                _pickableIncrementIndexes.Remove(0);
            Shuffle();
        };

        _incrementDatas[1].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.swordController.UpgradeSword();
            _incrementDatas[1].LevelUp();
            if (_incrementDatas[1].IsMaxLevel)
                _pickableIncrementIndexes.Remove(1);
            Shuffle();
        };

        _incrementDatas[2].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.swordController.IncreaseSpeed((int)_incrementDatas[2].CurrentValue);
            _incrementDatas[2].LevelUp();
            if (_incrementDatas[2].IsMaxLevel)
                _pickableIncrementIndexes.Remove(2);
            Shuffle();
        };

        _incrementDatas[3].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.swordController.AddSword((int)_incrementDatas[3].CurrentValue);
            _incrementDatas[3].LevelUp();
            if (_incrementDatas[3].IsMaxLevel)
                _pickableIncrementIndexes.Remove(3);
            Shuffle();
        };

        _incrementDatas[4].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.IncreaseSpeed((int)_incrementDatas[4].CurrentValue);
            _incrementDatas[4].LevelUp();
            if (_incrementDatas[4].IsMaxLevel)
                _pickableIncrementIndexes.Remove(4);
            Shuffle();
        };

        _incrementDatas[5].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.DecreaseSkillCoolDown((int)_incrementDatas[5].CurrentValue);
            _incrementDatas[5].LevelUp();
            if (_incrementDatas[5].IsMaxLevel)
                _pickableIncrementIndexes.Remove(5);
            Shuffle();
        };

        _incrementDatas[6].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.RecallHunter();
            _incrementDatas[6].LevelUp();
            if (_incrementDatas[6].IsMaxLevel)
            {
                _pickableIncrementIndexes.Remove(6);
                _pickableIncrementIndexes.Add(10);
                _pickableIncrementIndexes.Add(11);
                _pickableIncrementIndexes.Add(12);
            }
            Shuffle();
        };

        _incrementDatas[7].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.RecallMagician();
            _incrementDatas[7].LevelUp();
            if (_incrementDatas[7].IsMaxLevel)
            {
                _pickableIncrementIndexes.Remove(7);
                _pickableIncrementIndexes.Add(14);
            }
            Shuffle();
        };

        _incrementDatas[8].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.RecallHealer();
            _incrementDatas[8].LevelUp();
            if (_incrementDatas[8].IsMaxLevel)
            {
                _pickableIncrementIndexes.Remove(8);
                _pickableIncrementIndexes.Add(16);
                _pickableIncrementIndexes.Add(17);
            }
            Shuffle();
        };

        _incrementDatas[9].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.RecallGuardian();
            _incrementDatas[9].LevelUp();
            if (_incrementDatas[9].IsMaxLevel)
            {
                _pickableIncrementIndexes.Remove(9);
                _pickableIncrementIndexes.Add(19);
                _pickableIncrementIndexes.Add(20);
            }
            Shuffle();
        };

        _incrementDatas[10].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Hunter>().LevelUp(_incrementDatas[10].CurrentValue);
            _incrementDatas[10].LevelUp();
            if (_incrementDatas[10].IsMaxLevel)
                _pickableIncrementIndexes.Remove(10);
            Shuffle();
        };

        _incrementDatas[11].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Hunter>().IncreaseDamage((int)_incrementDatas[11].CurrentValue);
            _incrementDatas[11].LevelUp();
            if (_incrementDatas[11].IsMaxLevel)
                _pickableIncrementIndexes.Remove(11);
            Shuffle();
        };

        _incrementDatas[12].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Hunter>().IncreaseShotCount((int)_incrementDatas[12].CurrentValue);
            _incrementDatas[12].LevelUp();
            if (_incrementDatas[12].IsMaxLevel)
                _pickableIncrementIndexes.Remove(12);
            Shuffle();
        };

        _incrementDatas[13].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Magician>().LevelUp((int)_incrementDatas[13].CurrentValue);
            _incrementDatas[13].LevelUp();
            if (_incrementDatas[13].IsMaxLevel)
                _pickableIncrementIndexes.Remove(13);
            Shuffle();
        };

        _incrementDatas[14].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Magician>().IncreaseDamage((int)_incrementDatas[14].CurrentValue);
            _incrementDatas[14].LevelUp();
            if (_incrementDatas[14].IsMaxLevel)
                _pickableIncrementIndexes.Remove(14);
            Shuffle();
        };

        _incrementDatas[15].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Healer>().LevelUp((int)_incrementDatas[15].CurrentValue);
            _incrementDatas[15].LevelUp();
            if (_incrementDatas[15].IsMaxLevel)
                _pickableIncrementIndexes.Remove(15);
            Shuffle();
        };

        _incrementDatas[16].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Healer>().IncreaseHealAmount((int)_incrementDatas[16].CurrentValue);
            _incrementDatas[16].LevelUp();
            if (_incrementDatas[16].IsMaxLevel)
                _pickableIncrementIndexes.Remove(16);
            Shuffle();
        };

        _incrementDatas[17].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Healer>().DecreaseCoolDown((int)_incrementDatas[17].CurrentValue);
            _incrementDatas[17].LevelUp();
            if (_incrementDatas[17].IsMaxLevel)
                _pickableIncrementIndexes.Remove(17);
            Shuffle();
        };

        _incrementDatas[18].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Guardian>().LevelUp((int)_incrementDatas[18].CurrentValue);
            _incrementDatas[18].LevelUp();
            if (_incrementDatas[18].IsMaxLevel)
                _pickableIncrementIndexes.Remove(18);
            Shuffle();
        };

        _incrementDatas[19].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Guardian>().IncreaseDuration((int)_incrementDatas[19].CurrentValue);
            _incrementDatas[19].LevelUp();
            if (_incrementDatas[19].IsMaxLevel)
                _pickableIncrementIndexes.Remove(19);
            Shuffle();
        };

        _incrementDatas[20].OnLevelUp = () =>
        {
            Ingame.Instance.Hero.mercenaryController.GetMercenary<Guardian>().DecreaseCoolDown((int)_incrementDatas[20].CurrentValue);
            _incrementDatas[20].LevelUp();
            if (_incrementDatas[20].IsMaxLevel)
                _pickableIncrementIndexes.Remove(20);
            Shuffle();
        };
    }
}