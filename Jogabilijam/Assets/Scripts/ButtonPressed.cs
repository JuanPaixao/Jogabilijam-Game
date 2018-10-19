using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{

    [SerializeField] private GameObject _gate;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClip;
    private Animator _buttonAnimator;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _buttonAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _buttonAnimator.SetBool("isPressed", true);
            _gate.SetActive(false);
        }
    }
    public void ButtonDown()
    {
        _audioSource.PlayOneShot(_audioClip[0], 1f);
    }
    public void OpenTheGate()
    {
        _audioSource.PlayOneShot(_audioClip[1], 1f);
    }
}
