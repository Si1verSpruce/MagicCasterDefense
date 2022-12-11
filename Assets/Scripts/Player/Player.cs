using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

[RequireComponent(typeof(InstancePool))]
public class Player : MonoBehaviour, ISaveable
{
    [SerializeField] private int _health;

    private List<Spell> _spells = new List<Spell>();
    private int _money;
    private int _gems;
    private InstancePool _pool;

    public UnityAction<int> HealthChanged;
    public UnityAction<int> MoneyChanged;
    public UnityAction<int> GemsChanged;
    public UnityAction<Spell> SpellAdded;

    public int Health => _health;
    public int Money => _money;
    public int StageCoins => _gems;

    public void LoadState(string state)
    {
        var savedData = JsonUtility.FromJson<SaveData>(state);

        _money = savedData.money;
        _gems = savedData.gems;

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
    }

    public void BuySpell(Spell spell)
    {
        _gems -= spell.BuyPrice;

        GemsChanged?.Invoke(_gems);
        spell.Buy();
        _spells.Add(spell);
    }

    public void AddSpell(Spell spell)
    {
        _pool = GetComponent<InstancePool>();

        _spells.Add(spell);/*
        var instances = _pool.Expand(spell.InstanceToCreate);

        foreach (var instance in instances)
            if (instance.TryGetComponent<IScaleble>(out IScaleble scaleble))
                scaleble.Scale(spell.ScaleModifier);*/

        SpellAdded?.Invoke(spell);
    }

    public string SaveState()
    {
        SaveData data = new SaveData()
        {
            money = _money,
            gems = _gems
        };

        return JsonUtility.ToJson(data);
    }

    private struct SaveData
    {
        public int money;
        public int gems;
    }
}
