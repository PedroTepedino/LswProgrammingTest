using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //// Parameters
    [SerializeField] private float _moveSpeed;

    //// Unity Components
    [SerializeField] private Rigidbody2D _rigidbody;

    //// Custom Components
    private Mover _mover;
    
    private void Awake()
    {
        _mover = new Mover(_rigidbody, _moveSpeed);
    }

    private void Update()
    {
        _mover.Tick();
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = this.GetComponent<Rigidbody2D>();
        }
    }
}


public class Mover
{
    private readonly float _speed;
    private Rigidbody2D _rigidbody;

    public Mover(Rigidbody2D rigidbody, float speed)
    {
        _rigidbody = rigidbody;
        this._speed = speed;
    }

    public void Tick()
    {
        _rigidbody.velocity = _speed * InputManager.MoveInput.normalized;
    }
}