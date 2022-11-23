using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private StageStoredDataHandler _dataHandler;
    [SerializeField] private int _health;
    [SerializeField] private List<Spell> _spells = new List<Spell>();

    private int _money;

    public UnityAction<int> MoneyChanged;
    public UnityAction<int> HealthChanged;

    public int Money => _money;

    private void Awake()
    {
        AddMoney(_dataHandler.PlayerMoney);
        ApplyDamage(0);
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

    public void ApplyDamage(int damage)
    {
        if (damage >= 0)
        {
            _health -= damage;

            HealthChanged?.Invoke(_health);
        }
    }
}
