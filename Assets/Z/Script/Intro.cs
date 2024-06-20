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
        // 비동기적으로 Scene을 불러오기 위해 Coroutine을 사용한다.
        StartCoroutine(LoadMyAsyncScene());
    }

    IEnumerator LoadMyAsyncScene()
    {
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");
        asyncLoad.allowSceneActivation = false;

        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
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
