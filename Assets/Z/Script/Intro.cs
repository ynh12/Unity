using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    float time = 0;
    public GameObject logo;
    public GameObject loading;
    public GameObject button;
    public GameObject slider_;
    public Slider slider;

    public void StartGame()
    {
        LoadNextScene();
        logo.SetActive(false);
        loading.SetActive(true);
        slider_.SetActive(true);
        button.SetActive(false);
    }

    public void LoadNextScene()
    {
        // �񵿱������� Scene�� �ҷ����� ���� Coroutine�� ����Ѵ�.
        StartCoroutine(LoadMyAsyncScene());
    }

    IEnumerator LoadMyAsyncScene()
    {
        // AsyncOperation�� ���� Scene Load ������ �� �� �ִ�.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");
        asyncLoad.allowSceneActivation = false;

        // Scene�� �ҷ����� ���� �Ϸ�Ǹ�, AsyncOperation�� isDone ���°� �ȴ�.
        while (!asyncLoad.isDone)
        {
            time += Time.deltaTime;
            slider.value = time;
            if (time >= 10f)
                asyncLoad.allowSceneActivation = true;

            yield return null;
        }

        loading.SetActive(false);
        slider_.SetActive(false);
    }
}
