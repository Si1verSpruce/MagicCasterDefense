using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField] private SpellCombinationView _view;
    [SerializeField] private GameObject _screen;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _back;

    private List<SpellCombinationView> _views = new List<SpellCombinationView>();

    private void OnEnable()
    {
        _player.SpellAdded += InstantiateSpellView;
        _back.onClick.AddListener(DeactivateScreen);
    }

    private void OnDisable()
    {
        _player.SpellAdded -= InstantiateSpellView;
        _back.onClick.RemoveListener(DeactivateScreen);
    }

    public void ActivateScreen()
    {
        Time.timeScale = 0;
        _screen.SetActive(true);
    }

    private void InstantiateSpellView(Spell spell)
    {
        var newView = Instantiate(_view, _container);

        _views.Add(newView);
        newView.Init(spell);
    }

    private void DeactivateScreen()
    {
        Time.timeScale = 1;
        _screen.SetActive(false);
    }
}
