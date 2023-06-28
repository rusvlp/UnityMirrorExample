using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text Text;

    public Player Player;

    public void SetPlayer(Player _player)
    {
        this.Player = _player;
        this.Text.text = "Player " + this.Player.playerIndex;
    }
    
    void Start()
    {
        
    }

    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
