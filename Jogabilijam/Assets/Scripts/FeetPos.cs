using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetPos : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerAnimations _playerAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (_player.isJumping != true && !_player.isGrounded && other.gameObject.CompareTag("Ground"))
        {
            _playerAnim.isFalling(true);
        }
    }
}