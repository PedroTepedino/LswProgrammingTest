using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private readonly int _isMovingId = Animator.StringToHash("Moving");
    private readonly int _xVelocityId = Animator.StringToHash("XVelocity");
    private readonly int _yVelocityId = Animator.StringToHash("YVelocity");

    public bool IsMoving => _rigidbody2D.velocity.magnitude > 0.1f;

    private void LateUpdate()
    { 
        _animator.SetBool(_isMovingId, IsMoving);

        if (IsMoving)
        {
            _animator.SetFloat(_xVelocityId, _rigidbody2D.velocity.x);
            _animator.SetFloat(_yVelocityId, _rigidbody2D.velocity.y);
        }
    }

    private void OnValidate()
    {
        if (_animator == null)
        {
            _animator = this.GetComponentInChildren<Animator>();
        }

        if (_rigidbody2D == null)
        {
            _rigidbody2D = this.GetComponent<Rigidbody2D>();
        }
    }
}
