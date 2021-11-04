using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

#pragma warning disable 649

// sends input from PlayerInput to attached CharacterMovement components
public class CameraController : MonoBehaviour
{
    // Dolly Cam to use
    private CinemachineDollyCart _cart;

    // private ICharacterMovement _characterMovement;
    private Vector2 _moveInput;
    private bool    _held = false;

    private void Awake() {
        _cart = GetComponent<CinemachineDollyCart>();
    }

    public void OnMove(InputValue value) {

        _moveInput = value.Get<Vector2>();
        _held = !_held;
    }

    private void Update() {

        if (_held)
            _cart.m_Position += _moveInput.x;
    }
}
