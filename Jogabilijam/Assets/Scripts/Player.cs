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
    public bool isGrounded;
    public float checkRadius;
    public LayerMask ground;
    private float movHor;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movHor = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(_feetPos.position, checkRadius, ground);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Oi");
            _rb.velocity = Vector2.up * _jumpForce;
        }
    }
    void FixedUpdate()
    {
        //  _rb.MovePosition(_rb.position + _movement * _speed * Time.deltaTime);
        _rb.velocity = new Vector2(movHor * _speed * Time.deltaTime, _rb.velocity.y);
    }
}
