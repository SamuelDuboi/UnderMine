using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Vector2 movement;
    public void Movement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (movement != Vector2.zero)
        {
            transform.Translate(movement * speed * Time.deltaTime);
            TileGenerator.instance.CheckPos(transform.position);
        }
    }
}
