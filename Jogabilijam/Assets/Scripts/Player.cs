using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Vector2 _movement;
    [SerializeField] private Transform _feetPos, _upperHeadPos;
    public bool isGrounded, isCrouching, isMoving, canClimb, isJumping, isPreparingJump,isFalling;
    public float checkGrounded, checkDistanceLedge, fallSpeed;
    public LayerMask ground;
    public float movHor, movVer;
    private Animator _animator;
    public string movingDirection;
    private RaycastHit _hit;
    private Vector3 move;
    private Rigidbody _rb;
    private Transform _transform;

    private Vector3 moveDirection = Vector3.zero;
    private PlayerAnimations _playerAnimations;

    void Start()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        movingDirection = "right";
    }
    void Update()
    {
        Movement();
        Debug.DrawRay(_upperHeadPos.position, Vector3.up, Color.yellow, 2f);
        if (isJumping)
        {
            if ((Physics.Raycast(_feetPos.position, Vector3.down, checkGrounded * 1.75f, ground)))
            {
                _playerAnimations.isFalling(true);
            }
        }
    }
    private void FixedUpdate()
    {
        if (!isPreparingJump && !isFalling)
        {
            if (!isCrouching || isJumping)
            {
                _rb.velocity = new Vector3(movHor * _speed * Time.deltaTime, _rb.velocity.y, 0);
            }
            else if (isCrouching)
            {
                _rb.velocity = new Vector3(movHor * _speed / 2 * Time.deltaTime, _rb.velocity.y, 0);
            }
        }
        if (!canClimb)
        {
            _rb.AddForce(Vector2.down * 19.8f);
        }
    }

    private void Movement()
    {
        movHor = Input.GetAxisRaw("Horizontal");
        movVer = Input.GetAxisRaw("Vertical");
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        isGrounded = (Physics.Raycast(_feetPos.position, Vector3.down, checkGrounded, ground));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping != true && !Physics.Raycast(_upperHeadPos.position, Vector3.up, out _hit, 0.5f, ground))
            {
                _playerAnimations.isJumping(true);
            }
        }

        isMoving = (movHor == 1 || movHor == -1) ? true : false;
        if (movHor > 0.1)
        {
            movingDirection = "right";
        }
        else if (movHor < -0.1)
        {
            movingDirection = "left";
        }
        _transform.transform.eulerAngles = (movingDirection == "right") ? new Vector3(_transform.transform.eulerAngles.x, -220, _transform.transform.eulerAngles.z) :
              new Vector3(_transform.transform.eulerAngles.x, -440, 0);

        if (Physics.Raycast(_upperHeadPos.position, Vector3.up, out _hit, 0.5f, ground))
        {
            Debug.Log(_hit.transform.name);
        }
        isCrouching = ((isGrounded && movVer < -0.01f) || Physics.Raycast(_upperHeadPos.position, Vector3.up, out _hit, 0.75f, ground)) ? true : false;

        if (movVer < -0.25f)
        {
            canClimb = false;
        }
        if (isJumping)
        {
            StartCoroutine(JumpCoroutine());
        }
    }
    public void FinishedClimb()
    {
        _animator.SetBool("isClimbing", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        canClimb = true;
        //set character velocity to 0;
    }
    private void OnTriggerExit(Collider other)
    {
        canClimb = false;
    }
    public void Jump()
    {
        if (isJumping != true && !Physics.Raycast(_upperHeadPos.position, Vector3.up, out _hit, 0.5f, ground))
        {
            isJumping = true;
            isPreparingJump = false;
            _rb.velocity = Vector3.up * _jumpForce * Time.fixedDeltaTime;
        }
    }
    public void PreparingJump()
    {
        isPreparingJump = true;
        _rb.velocity = Vector3.zero;
    }
    private IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        if (isGrounded)
        {
            isJumping = false;
            _playerAnimations.isJumping(false);
        }
    }
    public void DisableIsFalling()
    {
        _playerAnimations.isFalling(false);
    }
    public void SetSpeedToZero()
    {
        _rb.velocity = Vector3.zero;
    }
}
