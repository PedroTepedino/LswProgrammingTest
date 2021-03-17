﻿using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static Vector2 MoveInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    public static event Action OnInventoryAction;

    public static event Action OnInteract;

    public static event Action OnEscape;

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            OnInventoryAction?.Invoke();
        }

        if(Input.GetButtonDown("Interact"))
        {
            OnInteract?.Invoke();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            OnEscape?.Invoke();
        }
    }
}
