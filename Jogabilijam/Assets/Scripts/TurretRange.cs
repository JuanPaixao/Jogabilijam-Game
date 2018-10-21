using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRange : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().isHiding != true)
            {
                other.GetComponent<Player>().isCaptured = true;
                StartCoroutine(Stop());
            }
        }
    }
    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.06f);
        _animator.enabled = false;
    }
}
