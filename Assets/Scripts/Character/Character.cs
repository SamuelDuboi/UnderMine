using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerMoney currentPlayer;
  
    internal virtual void Start()
    {
        playerMovement.doubleClik += DoubleClick;
    }
    internal virtual void Update()
    {
        
    }
    internal virtual void DoubleClick()
    {
        
    }
}
