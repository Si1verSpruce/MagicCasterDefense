using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arsenal : MonoBehaviour, ISaveable
{
    [SerializeField] private Spell[] _spells;
    [SerializeField] private SpellView _spellView;
    [SerializeField] private Transform _spellContainer;
    [SerializeField] private Button _back;
    [SerializeField] private Player _player;

    public void LoadState(string state)
    {
        var data = JsonUtility.FromJson<SaveData>(state);

        _spells = data.spells;

        foreach (var spell in _spells)
        {
            var spellView = Instantiate(_spellView, _spellContainer);
            spellView.Init(spell);
            spellView.BuyButtonClicked += OnBuyButton;

            if (spell.IsBought)
                _player.AddSpell(spell);
        }
    }

    private void OnEnable()
    {
        _back.onClick.AddListener(Deactivate);
    }

    private void OnDisable()
    {
        _back.onClick.RemoveListener(Deactivate);
    }

    public string SaveState()
    {
        SaveData data = new SaveData()
        {
            spells = _spells
        };

        return JsonUtility.ToJson(data);
    }


    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnBuyButton(Spell spell, SpellView view)
    {
        if (_player.StageCoins >= spell.BuyPrice)
        {
            _player.BuySpell(spell);
            spell.Buy();
            view.UpdateState();
        }
    }

    private struct SaveData
    {
        public Spell[] spells;
    }
}
