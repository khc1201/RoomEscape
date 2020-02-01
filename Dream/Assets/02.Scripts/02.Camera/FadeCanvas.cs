using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    RawImage fadeImage;
    bool isFaded = false;
    bool isEnd = false;
    public bool IsEnd { get { return isEnd; } set { isEnd = value; } }
    public float fadeTime = 3.0f;
    private void Start()
    {
        InitFade();
    }
    private void InitFade(bool isFirstTime = true)
    {

        fadeImage = this.GetComponentInChildren<RawImage>();
        fadeImage.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
        HideFadeObject();

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

    public void StartFadeIn(bool isImmediately)
    {
        ShowFadeObject();
        fadeImage.canvasRenderer.SetAlpha(0.0f);
        isEnd = false;

        if (isImmediately)
        {
            fadeImage.canvasRenderer.SetAlpha(1.0f);
            isFaded = true;
            isEnd = true;
            return;
        }

        if (isFaded)
        {
            Debug.LogError("이미 FadeIn 되어있다.");
            return;
        }
        fadeImage.CrossFadeAlpha(1.0f, fadeTime, false);
        StartCoroutine(ChangeFadeStatus(true));
    }

    IEnumerator ChangeFadeStatus(bool isfaded)
    {
        //yield return new WaitForSeconds(fadeTime* 10 * Time.deltaTime);
        yield return new WaitForSeconds(fadeTime + 0.5f);

        isFaded = isfaded;
        isEnd = true;

        if (!isFaded)
        {
            HideFadeObject();
        }
    }

    public void StartFadeOut(bool isImmediately)
    {
        isEnd = false;
        if (isImmediately)
        {
            fadeImage.canvasRenderer.SetAlpha(0f);
            isFaded = false;
            isEnd = true;
            return;
        }
        if (!isFaded)
        {
            Debug.LogError("이미 FadeOut 되었다.");
            return;
        }

        fadeImage.CrossFadeAlpha(0.0f, fadeTime, true);
        StartCoroutine(ChangeFadeStatus(false));
    }
}
