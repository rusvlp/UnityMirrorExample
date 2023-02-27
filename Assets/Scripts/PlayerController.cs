using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
   [SyncVar] 
   [SerializeField] 
   public float speed;
   
   private Rigidbody rb;


   private void Start()
   {
      print("Player initialized");
      
      rb = this.GetComponent<Rigidbody>();

      if (isClient && isLocalPlayer)
      {
         SetInputManagerPlayer();
      }
      if (isServer)
      {
         speed = 2;
      }
   }

   private void SetInputManagerPlayer()
   {
      InputManager.Instance.SetPlayer(this);
   }
   
   [Command]
   public void CmdMovePlayer(Vector3 movementVector)
   {
      rb.AddForce(movementVector.normalized * 10);
      //print(movementVector.x + " " + movementVector.y);
    }
}
