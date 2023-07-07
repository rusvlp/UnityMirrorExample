using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.Basic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update

    #region Singleton
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    private Vector3 movementVector = new Vector3();
    
    [SerializeField]
    private PlayerController playerObj;
    
    void Start()
    {
        print("Input manager initialized");
        
        playerObj = PlayerController.LocalPlayerController;
        
        _instance = this;
    }

    // Update is called once per frame

    private void Update()
    {
        MoveInput();  
    }
    
    
    private void MoveInput()
    {
        
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

       
        
        if (this.playerObj != null)
        {
            playerObj.CmdMovePlayer(movementVector);
            playerObj.MovePlayer(movementVector);
        }
    }

    public void SetPlayer(PlayerController pl)
    {
       playerObj = pl;
    }

}
