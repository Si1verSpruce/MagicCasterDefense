using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Spell : MonoBehaviour
{
    protected const float Lifetime = 10f;

    [SerializeField] private List<MagicElement> _combination = new List<MagicElement>();
    [SerializeField] private float _damage;
    [SerializeField] private float _duration;

    private float _currentLifetime;

    protected float Damage => _damage;

    private void Awake()
    {
        _currentLifetime = 0;
    }

    private void Update()
    {
        if (_currentLifetime >= _duration)
        {
            Deactivate();

            if (_currentLifetime >= Lifetime)
                Destroy(gameObject);
        }

        _currentLifetime += Time.deltaTime;
    }

    public bool CompareCombinations(List<MagicElement> combination)
    {
        if (_combination.Count() == combination.Count())
        {
            var spellCombination = _combination.OrderBy(element => element.Type).ToArray();
            var receivedCombination = combination.OrderBy(element => element.Type).ToArray();

            for (int i = 0; i < spellCombination.Count(); i++)
                if (spellCombination[i].Type != receivedCombination[i].Type)
                    return false;
        }
        else
        {
            return false;
        }

        return true;
    }

    protected abstract void Deactivate();

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.ApplyDamage(_damage);
    }
}
