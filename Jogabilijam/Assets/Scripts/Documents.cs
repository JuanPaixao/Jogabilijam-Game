using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Documents : MonoBehaviour
{

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _docAudio;
    void Start()
    {
		_audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_docAudio, 1f);
			_audioSource.PlayOneShot(_docAudio, 1f);
            Destroy(this.gameObject, 0.2f);

        }
    }
}
