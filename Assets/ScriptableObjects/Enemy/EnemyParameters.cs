using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProbabilityChange
{
    Decrease,
    Constant,
    Increase
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy", order = 51)]
public class EnemyParameters : ScriptableObject
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private float _fallUndergroundDelay;
    [SerializeField] private float _deactivateDelay;
    [SerializeField, Range(0, 1)] private float _baseSpawnChance;
    [SerializeField] private ProbabilityChange _spawnChanceChange;
    [SerializeField, Range(0, 1)] private float _spawnChancePerStageModifier;

    public float MoveSpeed => _moveSpeed;
    public int Damage => _damage;
    public int Reward => _reward;
    public float FallUndergroundDelay => _fallUndergroundDelay;
    public float DeactivateDelay => _deactivateDelay;
    public float BaseSpawnChance => _baseSpawnChance;
    public ProbabilityChange SpawnChanceChange => _spawnChanceChange;
    public float FallChancePerLevelModifier => _spawnChancePerStageModifier;
}
