using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
       // virtualCamera.m_Lens.FieldOfView = 30;
    }

    public void ZoomIn()
    {
        StartCoroutine(ZoomInCoroutine());
    }
    public void ZoomOut()
    {

        StartCoroutine(ZoomOutCoroutine());
    }
    IEnumerator ZoomInCoroutine()
    {
        while (virtualCamera.m_Lens.FieldOfView >= 20)
        {
            virtualCamera.m_Lens.FieldOfView--;
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator ZoomOutCoroutine()
    {
        while (virtualCamera.m_Lens.FieldOfView <= 30)
        {
            virtualCamera.m_Lens.FieldOfView++;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
