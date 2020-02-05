using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    RawImage fadeImage;
    [SerializeField] bool isFaded = false;
    bool isEnd = false;
    public bool IsEnd { get { return isEnd; } set { isEnd = value; } }
    public float fadeTime = 3.0f;
    [SerializeField] ReactObject targetObj;

    private void Start()
    {
        InitFade();
    }

    private void InitFade()
    {
        fadeImage = this.GetComponentInChildren<RawImage>();
        fadeImage.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
        isEnd = false;
        fadeImage.enabled = false;
    }


    public void StartFadeIn(ReactObject targetObject)
    {
        targetObj = targetObject;
        fadeImage.color = new Color(0, 0, 0, 0);
        if (targetObject.isImmediately)
        {
            fadeImage.DOFade(1.0f, 0.0f).OnStart(StartFadeIn_OnStart).OnComplete(StartFadeIn_OnEnd).Play();
        }
        else
        {
            fadeImage.DOFade(1.0f,fadeTime).OnStart(StartFadeIn_OnStart).OnComplete(StartFadeIn_OnEnd).Play();
        }
    }
    private void StartFadeIn_OnStart()
    {
        isEnd = false;
        fadeImage.enabled = true;
    }
    private void StartFadeIn_OnEnd()
    {
        isFaded = true;
        isEnd = true;
        targetObj.DoEnd_Fade();
    }



    public void StartFadeOut(ReactObject targetObject)
    {
        targetObj = targetObject;
        if (targetObject.isImmediately)
        {
            fadeImage.DOFade(0.0f, 0.0f).OnStart(StartFadeOut_OnStart).OnComplete(StartFadeOut_OnEnd).Play();
        }
        else
        {
            fadeImage.DOFade(0.0f, fadeTime).OnStart(StartFadeOut_OnStart).OnComplete(StartFadeOut_OnEnd).Play();
        }
    }
    private void StartFadeOut_OnStart()
    {
        isEnd = false;
        fadeImage.enabled = true;
    }
    private void StartFadeOut_OnEnd()
    {
        isFaded = true;
        isEnd = true;
        targetObj.DoEnd_Fade();
        isEnd = false;
        fadeImage.enabled = false;
    }
}
