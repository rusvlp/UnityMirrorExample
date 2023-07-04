using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : NetworkBehaviour
{
    private List<Player> _players = new List<Player>();

    public void AddPlayer(Player _player)
    {
        _players.Add(_player);
    }
}
