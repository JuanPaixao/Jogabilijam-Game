using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed, auxSpeed;
    [SerializeField] private float _jumpForce;
    private Vector2 _movement;
    [SerializeField] private Transform _feetPos, _upperHeadPos, _transform;
    public bool isGrounded, isCrouching, isMoving, canClimb, isJumping, isPreparingJump, isFalling;
    public float checkGrounded, checkDistanceLedge, fallSpeed;
    public LayerMask ground, climb;
    public float movHor, movVer;
    private Animator _animator;
    public string movingDirection;
    private RaycastHit _hit, _hitClimb;
    private Vector3 move;
    private Rigidbody _rb;
    private Vector3 direction;
    private PlayerAnimations _playerAnimations;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClip;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        auxSpeed = _speed;
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
        if (isJumping && isMoving)
        {
            _speed = auxSpeed + 20;
        }
        else
        {
            _speed = auxSpeed;
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce(Vector2.down * 15.5f);
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
        if (isJumping && isMoving)
        {
            if (movHor > 0.01f || movHor < 0.01f)
            {
                _rb.velocity = new Vector3(movHor * _speed * 1.5f * Time.deltaTime, _rb.velocity.y * 1.001f, 0);
            }
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
            if (isJumping != true && !Physics.Raycast(_upperHeadPos.position, Vector3.up, out _hit, 0.5f, ground) && !canClimb)
            {
                _playerAnimations.isJumping(true);
            }
        }
        isMoving = (movHor != 0) ? true : false;
        _playerAnimations.isMoving(isMoving);
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
        if (isGrounded && isFalling)
        {
            StartCoroutine(FallingCoroutine());
        }
        
    }
    public void Jump()
    {
        _audioSource.PlayOneShot(_audioClip[2], 0.25f);

        if (isJumping != true && !Physics.Raycast(_upperHeadPos.position, Vector3.up, out _hit, 0.5f, ground))
        {
            if (isMoving)
            {
                isJumping = true;
                isPreparingJump = false;
                _rb.velocity = Vector3.up * _jumpForce * Time.fixedDeltaTime;

            }
            else
            {
                isJumping = true;
                isPreparingJump = false;
                _rb.velocity = Vector3.up * _jumpForce * 0.952f * Time.fixedDeltaTime;
            }
        }
    }
    public void PreparingJump()
    {
        _rb.velocity = Vector3.zero;
        isPreparingJump = true;
    }
    private IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
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
    public void StepLeftCrouch()
    {
        _audioSource.PlayOneShot(_audioClip[0], 0.8f);
    }
    public void StepRightCrouch()
    {
        _audioSource.PlayOneShot(_audioClip[1], 0.8f);
    }
    public void StepLeftUp()
    {
        _audioSource.PlayOneShot(_audioClip[0], 1f);
    }
    public void StepRightUp()
    {
        _audioSource.PlayOneShot(_audioClip[1], 1f);
    }
    public IEnumerator FallingCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        isFalling = false;
        _playerAnimations.isFalling(false);
    }
}
