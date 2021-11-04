using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterMovement
{
    // methods
    void Jump();
    void SetMoveInput(Vector3 input);
    void SetLookDirection(Vector3 direction);

    // properties
    Vector3 Velocity { get; }
    Vector3 MoveInput { get; }
    Vector3 LocalMoveInput { get; }
    Vector3 LookDirection { get; }
    float Speed { get; }
    bool HasMoveInput { get; }
    bool IsGrounded { get; }
    bool IsAttacking { get; }
}