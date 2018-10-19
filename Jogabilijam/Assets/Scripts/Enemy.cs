using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float _enemySpeed;
    public bool isWalking, isPlayerLocated, isPlayerOnRange, playerCaptured;
    [SerializeField]
    private string _direction, _lookingDirection;
    private Rigidbody _rbEnemy;
    private Animator _animator;
    private Transform _transform;
    private RaycastHit _hit;
    [SerializeField]
    private Player _player;
    public float viewRange, auxViewRange;
    public LayerMask layerMask;
    public Light light;
    [SerializeField]
    private Transform _eye, _eye2;
    void Start()
    {
        auxViewRange = viewRange;
        _transform = GetComponent<Transform>();
        _rbEnemy = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        playerCaptured = false;
        _animator.SetBool("PlayerCaptured", false);
        int random = Random.Range(1, 3);
        _direction = (random == 1) ? "left" : "right";
        _lookingDirection = _direction;
        _transform.transform.eulerAngles = (_direction == "right") ? new Vector3(_transform.transform.eulerAngles.x, -280, _transform.transform.eulerAngles.z) :
  new Vector3(_transform.transform.eulerAngles.x, -460, 0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _rbEnemy.velocity = new Vector3(-_enemySpeed * Time.deltaTime, _rbEnemy.velocity.y, 0);
        }
        if (isWalking)
        {
            _lookingDirection = _direction;
        }
        CheckPlayer();

        if (playerCaptured)
        {
            light.color = Color.red;
            light.intensity = 100;
            isPlayerOnRange = true;
        }
    }

    void FixedUpdate()
    {
        if (_direction == "left" && isWalking && !isPlayerOnRange)
        {
            _rbEnemy.velocity = new Vector3(-_enemySpeed * Time.deltaTime, _rbEnemy.velocity.y, 0);
            _transform.transform.eulerAngles = (_direction == "right") ? new Vector3(_transform.transform.eulerAngles.x, -280, _transform.transform.eulerAngles.z) :
    new Vector3(_transform.transform.eulerAngles.x, -460, 0);
        }
        else if (_direction == "right" && isWalking && !isPlayerOnRange)
        {
            _rbEnemy.velocity = new Vector3(_enemySpeed * Time.deltaTime, _rbEnemy.velocity.y, 0);
            _transform.transform.eulerAngles = (_direction == "right") ? new Vector3(_transform.transform.eulerAngles.x, -280, _transform.transform.eulerAngles.z) :
    new Vector3(_transform.transform.eulerAngles.x, -460, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Right")
        {
            isWalking = false;
            _animator.SetBool("isIdle", true);
            _animator.SetBool("isWalking", false);
            _direction = "left";
        }
        else if (other.gameObject.name == "Left")
        {
            isWalking = false;
            _animator.SetBool("isIdle", true);
            _animator.SetBool("isWalking", false);
            _direction = "right";
        }
    }
    public IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(1.25f);

        light.color = Color.yellow;
        light.intensity = 40;
        isPlayerOnRange = false;
        _animator.SetBool("isPlayerOnRange", false);
    }
    public void Idle()
    {
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isIdle", true);
    }
    public void Walk()
    {
        isWalking = true;
        _animator.SetBool("isWalking", true);
        _animator.SetBool("isIdle", false);
    }
    public void PlayerCaptured()
    {
        if (isPlayerOnRange && !playerCaptured)
        {
            playerCaptured = true;
            _animator.SetBool("PlayerCaptured", true);
        }
    }
    private void CheckPlayer()
    {
        if (_lookingDirection == "right")
        {
            if (Physics.Raycast(_eye.transform.position, Vector3.right, out _hit, viewRange, layerMask) || Physics.Raycast(_eye2.transform.position, Vector3.right, out _hit, viewRange, layerMask))
            {
                if (_hit.collider != null)
                {
                    if (_hit.collider.CompareTag("Player"))
                    {
                        light.intensity = 70;
                        light.color = Color.red;
                        light.range = 15;
                        viewRange = auxViewRange + 3.5f;
                        isPlayerOnRange = true;
                        isWalking = false;
                        _animator.SetBool("isPlayerOnRange", true);
                    }
                }
            }
            else
            {
                viewRange = auxViewRange;
                light.color = Color.yellow;
                light.intensity = 40;
                light.range = 10;
                isPlayerOnRange = false;
                _animator.SetBool("isPlayerOnRange", false);
            }
        }
        else
        {
            if (Physics.Raycast(_eye.transform.position, Vector3.left, out _hit, viewRange, layerMask) || Physics.Raycast(_eye2.transform.position, Vector3.left, out _hit, viewRange, layerMask))
            {
                if (_hit.collider != null)
                {
                    if (_hit.collider.CompareTag("Player"))
                    {
                        light.color = Color.red;
                        light.intensity = 70;
                        light.range = 15;
                        viewRange = auxViewRange + 3.5f;
                        isWalking = false;
                        isPlayerOnRange = true;
                        _animator.SetBool("isPlayerOnRange", true);
                    }
                }
            }
            else
            {
                viewRange = auxViewRange;
                light.color = Color.yellow;
                light.intensity = 40;
                light.range = 10;
                isPlayerOnRange = false;
                _animator.SetBool("isPlayerOnRange", false);
            }
        }
    }
}
