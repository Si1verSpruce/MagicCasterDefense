using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Spell _currentSpell;

    public Spell CurrentSpell => _currentSpell;
}
