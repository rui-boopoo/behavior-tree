using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _facingDirection = Vector3.right;

    [Header("Horizontal Movement")] [SerializeField]
    private float _speed = 2.0f;

    [Header("Jump")] [SerializeField] private float _jumpForce = 2.0f;
    private bool _jumpButtonPressed;


    [Header("Reference")] [SerializeField] private Rigidbody _rigidbody;


    [UsedImplicitly]
    private void Update()
    {
        if (_jumpButtonPressed != true) _jumpButtonPressed = Input.GetButtonDown("Jump");
    }

    [UsedImplicitly]
    private void FixedUpdate()
    {
        HorizontalMove();
        Jump();
    }

    private void HorizontalMove()
    {
        float movingDirectionX = Input.GetAxis("Horizontal");
        float targetSpeed = movingDirectionX * _speed;
        Vector3 velocity = _rigidbody.velocity;

        velocity.x = Mathf.Lerp(velocity.x, targetSpeed, Time.fixedDeltaTime * 10);
        _rigidbody.velocity = velocity;
    }

    private void Jump()
    {
        if (!_jumpButtonPressed) return;
        _rigidbody.AddForce(_jumpForce * Vector3.up, ForceMode.Impulse);
        _jumpButtonPressed = false;
    }
}