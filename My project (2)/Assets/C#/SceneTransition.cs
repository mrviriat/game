using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

public class SceneTransition : MonoBehaviour
{
    //public Text LoadingPercentage;
    public Image LoadingProgressBar;


    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false;

    private Animator componentAnimator;
    private AsyncOperation loadingSceneOperation;

    public static async void LoadScene(string _index, Action<float> _progress, bool _activate = false)
    {
        UnityEngine.AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_index);
        asyncOperation.allowSceneActivation = _activate;
        while (asyncOperation.progress < ((!_activate) ? 0.9f : 1f))
        {
            Debug.Log($"[scene]:{_index} [load progress]: {asyncOperation.progress} {asyncOperation.isDone}");
            _progress(asyncOperation.progress);
            await Task.Yield();
        }
        if (!_activate) _progress(1f);
    }

    public static void SwitchToScene(string sceneName)
    {
        instance.componentAnimator.SetTrigger("startScene");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        // Чтобы сцена не начала переключаться пока играет анимация closing:
        instance.loadingSceneOperation.allowSceneActivation = false;

        instance.LoadingProgressBar.fillAmount = 0;
    }

    private void Start()
    {
        instance = this;

        componentAnimator = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation)
        {
            componentAnimator.SetTrigger("endScene");
            instance.LoadingProgressBar.fillAmount = 1;

            // Чтобы если следующий переход будет обычным SceneManager.LoadScene, не проигрывать анимацию opening:
            shouldPlayOpeningAnimation = false;
        }
    }

    private void Update()
    {
        if (loadingSceneOperation != null)
        {
            //LoadingPercentage.text = Mathf.RoundToInt((loadingSceneOperation.progress) * 100) + "%";
            
            
            // Просто присвоить прогресс:
            //LoadingProgressBar.fillAmount = loadingSceneOperation.progress; 

            // Присвоить прогресс с быстрой анимацией, чтобы ощущалось плавнее:
            LoadingProgressBar.fillAmount = Mathf.Lerp(LoadingProgressBar.fillAmount, loadingSceneOperation.progress,
                Time.deltaTime * 5);
        }
    }

    public void OnAnimationOver()
    {
        // Чтобы при открытии сцены, куда мы переключаемся, проигралась анимация opening:
        shouldPlayOpeningAnimation = true;

        loadingSceneOperation.allowSceneActivation = true;
    }

}