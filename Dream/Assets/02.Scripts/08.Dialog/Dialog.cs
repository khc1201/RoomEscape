using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Dialog : MonoBehaviour
{
    private Button nextButton;
    private Text textBox;
    private GameObject backGround;
    private int nowIndex = 0;
    private StreamObject nowObject;

    public static Dialog singleton;
    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this);
            return;
        }

    }

    public void Start()
    {
        InitDialog();
    }

    

    private void InitDialog()
    {
        nextButton = this.GetComponentInChildren<Button>();
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(NextDialog);

        textBox = this.GetComponentInChildren<Text>();

        backGround = this.transform.GetChild(0).gameObject;

        this.GetComponent<RectTransform>().localPosition = Vector3.zero;

        SetActiveDialog(false);
    }

    private void SetActiveDialog(bool isActive)
    {
        nextButton.gameObject.SetActive(isActive);
        textBox.gameObject.SetActive(isActive);
        backGround.SetActive(isActive);
    }

    private string GetDialog(StringData targetString)
    {
        string tempString = targetString.Index + "의 값이 없습니다.";
        switch (UserData.singleton.m_optiondata.language)
        {
            case (int)enum_Language.Korean:
                {
                    tempString = targetString.Lang_1;
                    break;
                }
            case (int)enum_Language.English:
                {
                    tempString = targetString.Lang_2;
                    break;
                }
            case (int)enum_Language.Chinese:
                {
                    tempString = targetString.Lang_3;
                    break;
                }
            case (int)enum_Language.Japanese:
                {
                    tempString = targetString.Lang_4;
                    break;
                }
        }
        return tempString;
    }

    public void ShowDialog(StreamObject targetObject = null)
    {
        if(targetObject != null)
        {
            nowObject = targetObject;
        }

        if(nowObject.dialogs.Count == nowIndex)
        {
            EndDialog();
            return;
        }

        ShowDialog_SetColor();
        ShowDialog_PlaySoundEffect();


        textBox.DOText(GetDialog(StringDataManager.singleton.GetString(nowObject.dialogs[nowIndex])), 2.0f, false, ScrambleMode.None, null)
            .SetEase(Ease.Linear)
            .OnPlay(DisableButton)
            .OnComplete(ActiveButton);

        SetActiveDialog(true);

    }
    private void ShowDialog_PlaySoundEffect()
    {
        if(StringDataManager.singleton.GetString(nowObject.dialogs[nowIndex]).PlaySoundEffect != "")
        {
            SoundEffect tempSound = SoundEffectManager.singleton.list_soundEffect.Find(x => x.name == StringDataManager.singleton.GetString(nowObject.dialogs[nowIndex]).PlaySoundEffect);
            if(tempSound == null)
            {
                Debug.LogError(tempSound + "가 SoundEffectManager 의 List 에 없다!");
                return;
            }
            tempSound.sound.Play();
        }

    }

    private void ShowDialog_SetColor()
    {
        switch (StringDataManager.singleton.GetString(nowObject.dialogs[nowIndex]).FontColor)
        {
            case "":
                {
                    // 흰 색
                    textBox.color = Color.white; 
                    break;
                }
            case "1":
                {
                    textBox.color = Color.yellow;
                    break;
                }
            default:
                {
                    //for test
                    Debug.LogError("현재 지정한 색상의 FontColor 가 없습니다. 여기서 case 를 추가할 것.");
                    break;
                }
        }

    }
    public void DisableButton()
    {
        // 글이 출력되는 동안에는 nextButton 을 클릭할 수 없다!
        nextButton.enabled = false;
    }
    public void ActiveButton()
    {
        // 글이 다 출력된 이후에 nextButton 을 클릭할 수 있다!
        nextButton.enabled = true;
    }
    public void NextDialog()
    {
        ++nowIndex;

        textBox.DOText("", 0.0f);

        ShowDialog();
    }
    private void EndDialog()
    {
        SetActiveDialog(false);

        nowObject.OjbectAction_Talk_onComplete();

        nowIndex = 0;
        nowObject = null;
    }
}
