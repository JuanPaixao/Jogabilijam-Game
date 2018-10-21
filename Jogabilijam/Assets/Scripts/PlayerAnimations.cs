using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator _animator;
    [SerializeField] private Player _player;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _animator.SetFloat("HorizontalMovement", Mathf.Abs(_player.movHor));
        _animator.SetBool("isGrounded", _player.isGrounded);
        _animator.SetBool("isCrouching", _player.isCrouching);
        _animator.SetFloat("VerticalMovement", Mathf.Abs(_player.movVer));
    }
    public void isJumping(bool jump)
    {
        _animator.SetBool("isJumping", jump);
    }
    public void isFalling(bool fall)
    {
        _animator.SetBool("isFalling", fall);
        _player.isFalling = fall;
    }
    public void isMoving(bool mov)
    {
        _animator.SetBool("isMoving", mov);
    }
    public void isCaptured(bool cap)
    {
        _animator.SetBool("isCaptured",cap);
    }
}