using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerClothesManager _playerClothesManager;

    private readonly int _isMovingId = Animator.StringToHash("Moving");
    private readonly int _xVelocityId = Animator.StringToHash("XVelocity");
    private readonly int _yVelocityId = Animator.StringToHash("YVelocity");

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
        var isMoving = _animator.GetBool(_isMovingId);
        var xVelocity = _animator.GetFloat(_xVelocityId);
        var yVelocity = _animator.GetFloat(_yVelocityId);   

        _animator.runtimeAnimatorController = clothing.Clothes;

        _animator.SetBool(_isMovingId, isMoving);
        _animator.SetFloat(_xVelocityId, xVelocity);
        _animator.SetFloat(_yVelocityId, yVelocity);
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
