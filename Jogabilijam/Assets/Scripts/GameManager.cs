using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;



public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public GameObject[] cutscene;
    public SpriteRenderer fade;
    public static int sceneNumber;
    private Scene sceneName;
    public GameObject gameOverSound;
    public static string language;
    public Camera cam1, cam2;

    void Start()
    {
        Debug.Log(language);
        sceneName = SceneManager.GetActiveScene();
        if (sceneName.name == "Menu")
        {
            sceneNumber = 0;
        }
        else if (sceneName.name == "Fase 01 - Esgoto")
        {
            sceneNumber = 1;
        }
        else if (sceneName.name == "Fase 02 - Segundo Andar")
        {
            sceneNumber = 2;
        }
        else if (sceneName.name == "Fase 03 - Terceiro Andar")
        {
            sceneNumber = 3;
        }
        else if (sceneName.name == ("End"))
        {
            sceneNumber = 4;

            if (language == "Portugues")
            {
                cutscene[0].SetActive(true);
            }
            else
            {
                cutscene[2].SetActive(true);
            }
        }
        if (sceneName.name == "GameOver")
        {
            if (gameOverSound != null)
            {
                StartCoroutine(StartGameOverSound());
            }
        }
        if (sceneName.name == "End")
        {
            StartCoroutine(FinalCutscene());
        }
        if (fade != null)
        {
            StartCoroutine(FadeRoutine());
        }
        Debug.Log(sceneNumber);
    }

    public void SetLanguage(string setLang)
    {
        language = setLang;
        SceneManager.LoadSceneAsync("Menu");
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
            cutscene[3].SetActive(false);
            cutscene[2].SetActive(false);
            cutscene[1].SetActive(false);
            if (language == "Portugues")
            {
                cutscene[0].SetActive(true);
            }
            else
            {
                cutscene[4].SetActive(true);
            }


            yield return new WaitForSecondsRealtime(47f);
            SceneManager.LoadSceneAsync("Fase 01 - Esgoto");
        }
    }
    public IEnumerator FadeRoutine()
    {
        float opacity = fade.color.a;
        while (opacity >= 0)
        {
            fade.color = new Color(this.fade.color.r, this.fade.color.g, this.fade.color.b, opacity);
            opacity -= 0.1f;
            yield return new WaitForSeconds(0.025f);
        }
    }
    public IEnumerator InverseFadeRoutine()
    {
        float opacity = fade.color.a;
        while (opacity <= 100)
        {
            fade.color = new Color(this.fade.color.r, this.fade.color.g, this.fade.color.b, opacity);
            opacity += 0.1f;
            yield return new WaitForSeconds(0.025f);
        }
    }
    public void StartInverseFade()
    {
        StartCoroutine(InverseFadeRoutine());
    }
    public void LoadNextScene(string Scene)
    {
        SceneManager.LoadSceneAsync(Scene);
    }
    public void PlayerCaptured()
    {
        SceneManager.LoadSceneAsync("GameOver");
    }
    public void GoBack()
    {
        if (sceneNumber == 1)
        {
            SceneManager.LoadSceneAsync("Fase 01 - Esgoto");
        }
        else if (sceneNumber == 2)
        {
            SceneManager.LoadSceneAsync("Fase 02 - Segundo Andar");
        }
        else if (sceneNumber == 3)
        {
            SceneManager.LoadSceneAsync("Fase 03 - Terceiro Andar");
        }
    }
    public void GoToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
    public IEnumerator StartGameOverSound()
    {
        yield return new WaitForSeconds(2);
        gameOverSound.SetActive(true);
    }
    public IEnumerator FinalCutscene()
    {
        yield return new WaitForSecondsRealtime(58f);
        SceneManager.LoadSceneAsync("Menu");
    }
    public void ChangeCamera()
    {
        cam1.enabled = false;
        cam2.enabled = true;
        Rigidbody player = GameObject.Find("Hero").GetComponent<Rigidbody>();
        player.isKinematic = true;
    }
    public void ReturnToMainCamera()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        Rigidbody player = GameObject.Find("Hero").GetComponent<Rigidbody>();
        player.isKinematic = false;
    }
}
