using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Player : MonoBehaviour, ISaveable
{
    [SerializeField] private int _health;

    private List<Spell> _spells = new List<Spell>();
    private int _money;
    private int _stageCoins;

    public UnityAction<int> HealthChanged;
    public UnityAction<int> MoneyChanged;
    public UnityAction<int> StageCoinsChanged;

    public int Health => _health;
    public int Money => _money;
    public int StageCoins => _stageCoins;

    public void LoadState(string state)
    {
        var savedData = JsonUtility.FromJson<SaveData>(state);

        _money = savedData.money;
        _stageCoins = savedData.stageCoins;

        AddMoney(0);
    }

    public Spell GetSpell(List<MagicElement> combination)
    {
        return _spells.FirstOrDefault(spell => spell.CompareCombinations(combination));
    }

    public void AddMoney(int money)
    {
        if (money >= 0)
        {
            _money += money;

            MoneyChanged?.Invoke(_money);
        }
    }

    public void AddStageCoin()
    {
        _stageCoins++;

        StageCoinsChanged?.Invoke(_stageCoins);
    }

    public void ApplyDamage(int damage)
    {
        if (damage >= 0)
        {
            _health -= damage;

            HealthChanged?.Invoke(_health);
        }
    }

    public void UpgradeSpell(Spell spell)
    {
        _money -= spell.UpgradePrice;
        spell.IncreaseLevel();

        MoneyChanged?.Invoke(_money);
    }

    public void BuySpell(Spell spell)
    {
        _stageCoins -= spell.BuyPrice;

        StageCoinsChanged?.Invoke(_stageCoins);
        spell.Buy();
        _spells.Add(spell);
    }

    public void AddSpell(Spell spell)
    {
        _spells.Add(spell);
    }

    public string SaveState()
    {
        SaveData data = new SaveData()
        {
            money = _money,
            stageCoins = _stageCoins
        };

        return JsonUtility.ToJson(data);
    }

    private struct SaveData
    {
        public int money;
        public int stageCoins;
    }
}
