using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    public GameManager gameManager;
    public Animator _animator;
    public bool triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!triggered)
            {
                Debug.Log(other.gameObject.name);
                _animator.SetBool("isOn", true);
                gameManager.ChangeCamera();
                triggered = true;
            }
        }
    }
    public void Return()
    {
        gameManager.ReturnToMainCamera();
    }
}
