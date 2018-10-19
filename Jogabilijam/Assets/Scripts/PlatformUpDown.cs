using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpDown : MonoBehaviour
{
    private Animator _animator;
    public string upOrDown;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ChangeDirectionPlatform());
        }
    }

    public IEnumerator ChangeDirectionPlatform()
    {
        yield return new WaitForSeconds(1f);
        if (upOrDown == "up")
            _animator.SetBool("PlatUp", false);

        else if (upOrDown == "down")
        {
            _animator.SetBool("PlatUp", true);
        }
        else
        {
            _animator.SetBool("PlatUp", true);
        }
    }
    public void Direction(string Direction)
    {
        upOrDown = Direction;
    }
    public IEnumerator PlatRoutine()
    {
        yield return new WaitForSeconds(5f);

        if (upOrDown == "up")
            _animator.SetBool("PlatUp", false);
    }
}

