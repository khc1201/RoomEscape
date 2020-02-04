using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    RawImage fadeImage;
    Color fadeinColor;
    Color fadeoutColoor;
    bool isFaded = false;
    bool isEnd = false;
    public bool IsEnd { get { return isEnd; } set { isEnd = value; } }
    public float fadeTime = 3.0f;
    ReactObject targetObj;

    Tween twFadeIn;
    Tween twFadeOut;

    private void Start()
    {
        InitFade();
    }
    private void InitFade(bool isFirstTime = true)
    {

        fadeImage = this.GetComponentInChildren<RawImage>();
        fadeImage.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
        HideFadeObject();
        fadeinColor = new Color(0f, 0f, 0f, 255f);
        fadeoutColoor = new Color(0f, 0f, 0f, 0f);
        //twFadeIn = fadeImage.DOFade(1, fadeTime).SetEase(Ease.Linear).OnStart(StartFadeIn_OnStart).OnComplete(StartFadeIn_OnEnd);
        //twFadeOut = fadeImage.DOFade(0, fadeTime).SetEase(Ease.Linear).OnComplete(HideFadeObject);
    }
    private void ShowFadeObject()
    {
        isEnd = false;
        fadeImage.gameObject.SetActive(true);
    }

    public void HideFadeObject()
    {
        isEnd = false;
        fadeImage.gameObject.SetActive(false);
    }

    public void StartFadeIn(ReactObject targetObject)
    {
        targetObj = targetObject;
        if (targetObject.isImmediately)
        {
            //for test
            Debug.Log(targetObject.gameObject.name + "의 isImmediately 가 true 이므로 즉시 실행");
            twFadeIn = fadeImage.DOColor(fadeinColor, 0.1f).OnStart(StartFadeIn_OnStart).OnComplete(StartFadeIn_OnEnd);
            //DOFade(1, 0.01f).OnStart(StartFadeIn_OnStart).OnComplete(StartFadeIn_OnEnd);
        }
        else
        {
            twFadeIn = fadeImage.DOColor(fadeinColor, fadeTime).SetEase(Ease.Linear).OnStart(StartFadeIn_OnStart).OnComplete(StartFadeIn_OnEnd);
            //twFadeIn = fadeImage.DOFade(1, fadeTime).SetEase(Ease.Linear).OnStart(StartFadeIn_OnStart).OnComplete(StartFadeIn_OnEnd);
        }
        twFadeIn.Play();

        /*
        ShowFadeObject();
        fadeImage.canvasRenderer.SetAlpha(0.0f);
        isEnd = false;

        if (targetObject.isImmediately)
        {
            fadeImage.canvasRenderer.SetAlpha(1.0f);
            isFaded = true;
            isEnd = true;
            targetObject.DoEnd_Fade();
            return;
        }

        if (isFaded)
        {
            Debug.LogError("이미 FadeIn 되어있다.");
            return;
        }
        fadeImage.CrossFadeAlpha(1.0f, fadeTime, false);
        StartCoroutine(ChangeFadeStatus(true, targetObject));
        */
    }
    private void StartFadeIn_OnStart()
    {
        //for test
        Debug.Log("StartFadeIn 시작 시에 발동!");
        ShowFadeObject();
        fadeImage.canvasRenderer.SetAlpha(0.0f);
        isEnd = false;
    }
    private void StartFadeIn_OnEnd()
    {
        //for test
        Debug.Log("StartFadeIn 완료 시에 발동!");
        isFaded = true;
        isEnd = true;
        targetObj.DoEnd_Fade();
    }
    /*
    IEnumerator ChangeFadeStatus(bool isfaded, ReactObject targetObject)
    {
        //yield return new WaitForSeconds(fadeTime* 10 * Time.deltaTime);
        yield return new WaitForSeconds(fadeTime + 0.5f);

        isFaded = isfaded;
        isEnd = true;
        targetObject.DoEnd_Fade();

        if (!isFaded)
        {
            HideFadeObject();
        }
    }
    */

    public void StartFadeOut(ReactObject targetObject)
    {
        twFadeOut.Restart();

        isEnd = false;
        if (targetObject.isImmediately)
        {
            fadeImage.canvasRenderer.SetAlpha(0f);
            isFaded = false;
            isEnd = true;
            targetObject.DoEnd_Fade();
            return;
        }
        if (!isFaded)
        {
            Debug.LogError("이미 FadeOut 되었다.");
            return;
        }

        fadeImage.CrossFadeAlpha(0.0f, fadeTime, true);
        //StartCoroutine(ChangeFadeStatus(false, targetObject));
    }
}
