using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCast : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void OnCast(Vector3 targetPosition)
    {
        Instantiate(_player.CurrentSpell, targetPosition, Quaternion.identity);
    }
}
