using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;



public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public GameObject[] cutscene;

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
    public void LSceneyAsyncCoroutine()
    {
        StartCoroutine(LoadSceneAsync());

    }
    public IEnumerator LoadSceneAsync()
    {
        if (cutscene != null)
        {
            cutscene[2].SetActive(false);
            cutscene[1].SetActive(false);
            cutscene[0].SetActive(true);
            yield return new WaitForSecondsRealtime(44f);
            SceneManager.LoadSceneAsync("Fase_01 - Esgoto");
        }
    }
}
