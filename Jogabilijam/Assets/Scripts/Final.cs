using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public GameManager gameManager;
    public string nextScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FinishThis());
        }
    }
    private IEnumerator FinishThis()
    {
        yield return new WaitForSecondsRealtime(3);
        gameManager.LoadNextScene(nextScene);
    }
}
