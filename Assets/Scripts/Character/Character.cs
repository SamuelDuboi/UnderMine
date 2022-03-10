using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    public PlayerMovement playerMovement;
   // public PlayerMoney currentPlayer;
  
    internal virtual IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
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
