using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    [SerializeField] private Transform _feetPos;
    public bool isGrounded, isCrouching;
    public float checkRadius;
    public LayerMask ground;
    private float movHor;
    private float movVer;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void FixedUpdate()
    {
        if (!isCrouching)
        {
            _rb.velocity = new Vector2(movHor * _speed * Time.deltaTime, _rb.velocity.y);
        }
    }

    private void Movement()
    {

        movHor = Input.GetAxis("Horizontal");
        movVer = Input.GetAxisRaw("Vertical");
        isGrounded = Physics2D.OverlapCircle(_feetPos.position, checkRadius, ground);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = Vector2.up * _jumpForce;
        }

        if (isGrounded && movVer < -0.5f)
        {
            _animator.SetBool("isCrouching", true);
            isCrouching = true;
        }
        else
        {
            _animator.SetBool("isCrouching", false);
            isCrouching = false;
        }
        if (isCrouching)
        {
            _rb.velocity = Vector2.zero;
        }
    }
}
