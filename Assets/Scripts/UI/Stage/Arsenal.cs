using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Arsenal : MonoBehaviour, ISaveable
{
    [SerializeField] private SaveLoadSystem _saveLoadSystem;
    [SerializeField] private GameObject _screen;
    [SerializeField] private Spell[] _spells;
    [SerializeField] private SpellView _spellView;
    [SerializeField] private Transform _spellContainer;
    [SerializeField] private Button _back;
    [SerializeField] private Player _player;

    public void LoadState(string state)
    {
        var data = JsonConvert.DeserializeObject<SaveData>(state);

        foreach (var spell in _spells)
        {
            data.spells.TryGetValue(spell.GetType().ToString(), out SpellData spellData);
            spell.Init(spellData.isBought, spellData.level);

            var spellView = Instantiate(_spellView, _spellContainer);
            spellView.Init(spell);

            if (spell.IsBought)
            {
                _player.AddSpell(spell);
                spellView.UpgradeButtonClicked += OnUpgradeButton;
            }
            else
            {
                spellView.BuyButtonClicked += OnBuyButton;
            }
        }
    }

    private void OnEnable()
    {
        _back.onClick.AddListener(DeactivateScreen);
    }

    private void OnDisable()
    {
        _back.onClick.RemoveListener(DeactivateScreen);
    }

    public string SaveState()
    {
        Dictionary<string, SpellData> spellDictionary = new Dictionary<string, SpellData>();

        foreach (var spell in _spells)
            spellDictionary[spell.GetType().ToString()] = new SpellData()
            {
                isBought = spell.IsBought,
                level = spell.Level
            };

        SaveData data = new SaveData()
        {
            spells = spellDictionary
        };

        return JsonConvert.SerializeObject(data);
    }

    public void ActivateScreen()
    {
        _screen.SetActive(true);
    }

    private void DeactivateScreen()
    {
        _screen.SetActive(false);
    }

    private void OnBuyButton(Spell spell, SpellView view)
    {
        if (_player.StageCoins >= spell.BuyPrice)
        {
            _player.BuySpell(spell);
            view.ActivateUpgradeGroup();
            view.BuyButtonClicked -= OnBuyButton;
            view.UpgradeButtonClicked += OnUpgradeButton;

            _saveLoadSystem.Save();
        }
    }

    private void OnUpgradeButton(Spell spell, SpellView view)
    {
        if (_player.Money >= spell.UpgradePrice)
        {
            _player.UpgradeSpell(spell);
            view.UpdateUpgradeGroup();

            _saveLoadSystem.Save();

            if (spell.Level == int.MaxValue)
                view.UpgradeButtonClicked -= OnUpgradeButton;
        }
    }

    private struct SaveData
    {
        public Dictionary<string, SpellData> spells;
    }

    private struct SpellData
    {
        public bool isBought;
        public int level;
    }
}
