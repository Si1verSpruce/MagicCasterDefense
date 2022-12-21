using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(SpellPool))]
public class Player : MonoBehaviour, ISaveable
{
    [SerializeField] private int _health;
    [SerializeField] private SaveLoadSystem _saveLoadSystem;

    private List<Spell> _spells = new List<Spell>();
    private int _money;
    private int _gems;
    private SpellPool _pool;

    public UnityAction<int> HealthChanged;
    public UnityAction<int> MoneyChanged;
    public UnityAction<int> GemsChanged;
    public UnityAction<Spell> SpellAdded;

    public int Health => _health;
    public int Money => _money;
    public int StageCoins => _gems;

    public void LoadState(string saveData)
    {
        var data = JsonUtility.FromJson<SaveData>(saveData);

        _money = data.money;
        _gems = data.gems;

        AddMoney(0);
    }

    public void LoadByDefault()
    {
        AddMoney(0);
    }

    public Spell GetSpell(List<ElementType> combination)
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

    public void AddGem()
    {
        _gems++;

        GemsChanged?.Invoke(_gems);
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
        spell.OnLevelIncrease();
        MoneyChanged?.Invoke(_money);

        _saveLoadSystem.Save(this);
    }

    public void BuySpell(Spell spell)
    {
        _gems -= spell.BuyPrice;
        GemsChanged?.Invoke(_gems);
        spell.Buy();
        _spells.Add(spell);

        _saveLoadSystem.Save(this);
    }

    public void AddSpell(Spell spell)
    {
        _pool = GetComponent<SpellPool>();

        _spells.Add(spell);
        var instances = _pool.Expand(spell.InstanceToCreate);

        foreach (var instance in instances)
            if (instance.TryGetComponent<IScaleble>(out IScaleble scaleble))
                scaleble.Scale(spell.ScaleModifier);

        SpellAdded?.Invoke(spell);
    }

    public object SaveState()
    {
        SaveData data = new SaveData()
        {
            money = _money,
            gems = _gems
        };

        return data;
    }

    [Serializable]
    private struct SaveData
    {
        public int money;
        public int gems;
    }
}
