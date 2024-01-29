using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 MoveAmount
    {
        get;
        private set;
    } 

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveAmount = context.ReadValue<Vector2>();
    }    
}
