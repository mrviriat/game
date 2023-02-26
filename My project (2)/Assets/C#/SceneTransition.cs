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

        // ����� ����� �� ������ ������������� ���� ������ �������� closing:
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

            // ����� ���� ��������� ������� ����� ������� SceneManager.LoadScene, �� ����������� �������� opening:
            shouldPlayOpeningAnimation = false;
        }
    }

    private void Update()
    {
        if (loadingSceneOperation != null)
        {
            //LoadingPercentage.text = Mathf.RoundToInt((loadingSceneOperation.progress) * 100) + "%";
            
            
            // ������ ��������� ��������:
            //LoadingProgressBar.fillAmount = loadingSceneOperation.progress; 

            // ��������� �������� � ������� ���������, ����� ��������� �������:
            LoadingProgressBar.fillAmount = Mathf.Lerp(LoadingProgressBar.fillAmount, loadingSceneOperation.progress,
                Time.deltaTime * 5);
        }
    }

    public void OnAnimationOver()
    {
        // ����� ��� �������� �����, ���� �� �������������, ����������� �������� opening:
        shouldPlayOpeningAnimation = true;

        loadingSceneOperation.allowSceneActivation = true;
    }

}