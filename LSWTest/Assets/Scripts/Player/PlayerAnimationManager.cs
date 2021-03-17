using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerClothesManager _playerClothesManager;

    private readonly int _isMovingId = Animator.StringToHash("Moving");
    private readonly int _xVelocityId = Animator.StringToHash("XVelocity");
    private readonly int _yVelocityId = Animator.StringToHash("YVelocity");

    private IShop _shopInRange;

    public bool IsMoving => _rigidbody2D.velocity.magnitude > 0.1f;

    private void OnEnable()
    {
        _playerClothesManager.OnCurrentOutfitChanged += ListenOnPlayerOutfitChanged;
    }

    private void OnDisable()
    {
        _playerClothesManager.OnCurrentOutfitChanged -= ListenOnPlayerOutfitChanged;
    }

    private void LateUpdate()
    {
        if (Time.timeScale < 0.1) return;

        _animator.SetBool(_isMovingId, IsMoving);

        if (IsMoving)
        {
            _animator.SetFloat(_xVelocityId, _rigidbody2D.velocity.x);
            _animator.SetFloat(_yVelocityId, _rigidbody2D.velocity.y);
        }
    }

    

    private void ListenOnPlayerOutfitChanged(ClothingPiece clothing)
    {
        _animator.runtimeAnimatorController = clothing.Clothes;
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

        if(_playerClothesManager == null)
        {
            _playerClothesManager = this.GetComponent<PlayerClothesManager>();
        }
    }
}
