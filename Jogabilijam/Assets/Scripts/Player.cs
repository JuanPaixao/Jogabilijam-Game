using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Rigidbody _rb;
    private Vector2 _movement;
    [SerializeField] private Transform _feetPos;
    public bool isGrounded, isCrouching, isMoving, canClimb;
    public float checkGrounded;
    public LayerMask ground;
    private float movHor;
    private float movVer;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
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
        if (!canClimb)
        {
            _rb.AddForce(Vector2.down * 19.8f);
        }
    }

    private void Movement()
    {
        movHor = Input.GetAxis("Horizontal");
        movVer = Input.GetAxisRaw("Vertical");

        isMoving = (movHor > 0.1f || movHor < 0.1f) ? true : false;

        isGrounded = (Physics.Raycast(this.transform.position, Vector3.down, checkGrounded, ground));

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = Vector3.up * _jumpForce;
        }

        isCrouching = (isGrounded && movVer < -0.5f) ? true : false;

        if (isCrouching)
        {
            _rb.velocity = Vector3.zero;
        }
    }
    public void FinishedClimb()
    {
        _animator.SetBool("isClimbing", false);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            canClimb = true;
            _rb.velocity = Vector3.zero;
            _rb.AddForce(Vector2.up);
        }
    }
    void OnCollisionExit(Collision other)
    {
        canClimb = false;
    }
}
