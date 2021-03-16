using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    public static Vector2 MoveInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
}
