using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressedStg2 : MonoBehaviour
{

    [SerializeField] private GameObject _scannerDoor;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClip;
    private Animator _buttonAnimator;
    [SerializeField] private Animator _scannerAnimator;
    void Start()
    {
        _scannerAnimator = _scannerDoor.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _buttonAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _buttonAnimator.SetBool("isPressed", true);
            _scannerAnimator.SetBool("Open", true);
            StartCoroutine(PlaySoundOpen());
        }
    }
    private IEnumerator PlaySoundOpen()
    {
        _audioSource.PlayOneShot(_audioClip[0], 1f);
        yield return new WaitForSeconds(0.75f);
        _audioSource.PlayOneShot(_audioClip[1], 1f);
    }
}
