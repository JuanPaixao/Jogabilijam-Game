using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private CharacterController _characterController;
    private Vector2 _movement;
    [SerializeField] private Transform _feetPos, _upperHeadPos;
    public bool isGrounded, isCrouching, isMoving, canClimb;
    public float checkGrounded, checkDistanceLedge, fallSpeed;
    public LayerMask ground;
    public float movHor, movVer;
    private Animator _animator;
    public string movingDirection;
    private RaycastHit _hit;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        movingDirection = "right";
    }
    void Update()
    {
        Movement();
        Debug.DrawRay(_upperHeadPos.position, Vector3.up, Color.yellow, 5f);


    }
    void FixedUpdate()
    {

    }
    private void Movement()
    {
        movHor = Input.GetAxisRaw("Horizontal");
        movVer = Input.GetAxisRaw("Vertical");
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        if (!isCrouching)
        {
            Vector3 move = new Vector3(movHor * _speed, -fallSpeed, 0);
            _characterController.Move(move * Time.deltaTime);
        }
        else
        {
            Vector3 move = new Vector3(movHor * _speed / 2, -fallSpeed, 0);
            _characterController.Move(move * Time.deltaTime);
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
        if (Physics.Raycast(_upperHeadPos.position, Vector3.up, out _hit, 0.5f, ground))
        {
            Debug.Log(_hit.transform.name);
        }

        _characterController.transform.eulerAngles = (movingDirection == "right") ? new Vector3(_characterController.transform.eulerAngles.x, -220, _characterController.transform.eulerAngles.z) :
        new Vector3(_characterController.transform.eulerAngles.x, -440, 0);


        isGrounded = (Physics.Raycast(_feetPos.position, Vector3.down, checkGrounded, ground));

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            //jump  
        }

        isCrouching = ((isGrounded && movVer < -0.01f) || Physics.Raycast(_upperHeadPos.position, Vector3.up, out _hit, 0.5f, ground)) ? true : false;
        if (isCrouching)
        {
            //set character velocity to 0;
        }

        if (movVer < -0.25f)
        {
            canClimb = false;
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
}
