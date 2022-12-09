using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour, ISaveable
{
    [SerializeField] private SaveLoadSystem _saveLoadSystem;
    [SerializeField] private GameObject _screen;
    [SerializeField] private Spell[] _spells;
    [SerializeField] private SpellFullView _spellView;
    [SerializeField] private Transform _buyContainer;
    [SerializeField] private Transform _upgradeContainer;
    [SerializeField] private Button _back;
    [SerializeField] private Player _player;
    [SerializeField] private Button _switch;

    public void LoadState(string state)
    {
        var data = JsonConvert.DeserializeObject<SaveData>(state);

        foreach (var spell in _spells)
        {
            data.spells.TryGetValue(spell.GetType().ToString(), out SpellData spellData);
            spell.Init(spellData.isBought, spellData.level);

            SpellFullView spellView;

            if (spell.IsBought)
            {
                spellView = Instantiate(_spellView, _upgradeContainer);
                _player.AddSpell(spell);
                spellView.UpgradeButtonClicked += OnUpgradeButton;
            }
            else
            {
                spellView = Instantiate(_spellView, _buyContainer);
                spellView.BuyButtonClicked += OnBuyButton;
            }

            spellView.Init(spell);
        }
    }

    private void OnEnable()
    {
        _back.onClick.AddListener(DeactivateScreen);
        _switch.onClick.AddListener(SwitchScreen);
    }

    private void OnDisable()
    {
        _back.onClick.RemoveListener(DeactivateScreen);
        _switch.onClick.RemoveListener(SwitchScreen);
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

    private void OnBuyButton(Spell spell, SpellFullView view)
    {
        if (_player.StageCoins >= spell.BuyPrice)
        {
            _player.BuySpell(spell);
            view.ActivateUpgradeGroup();
            view.BuyButtonClicked -= OnBuyButton;
            view.UpgradeButtonClicked += OnUpgradeButton;
            view.transform.SetParent(_upgradeContainer);

            _saveLoadSystem.Save();
        }
    }

    private void OnUpgradeButton(Spell spell, SpellFullView view)
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

    private void SwitchScreen()
    {
        if (_buyContainer.gameObject.activeSelf)
        {
            _buyContainer.gameObject.SetActive(false);
            _upgradeContainer.gameObject.SetActive(true);
        }
        else
        {
            _buyContainer.gameObject.SetActive(true);
            _upgradeContainer.gameObject.SetActive(false);
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
