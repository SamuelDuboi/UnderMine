using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    public PlayerMovement playerMovement;
   // public PlayerMoney currentPlayer;
  
    internal virtual void Start()
    {
       
    }
    internal virtual void Update()
    {

    }
    public void CheckClick(InputAction.CallbackContext context)
    {
        if (context.performed)
            DoubleClick();
    }
    internal virtual void DoubleClick()
    {
        
    }
}
