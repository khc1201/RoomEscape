using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringChanger : MonoBehaviour, IListener
{
    public Text targetText;
    public string stringIndex;
    private StringData thisStringData;

    public void Start()
    {
        EventManager.Singleton.AddListener(enum_EventType.Change_Language, this);
        StartCoroutine(InitString());
    }

    void OnEnable()
    {
        if(thisStringData == null)
        {
            EventManager.Singleton.AddListener(enum_EventType.Change_Language, this);
            thisStringData = StringDataManager.singleton.GetString(stringIndex);
            SetString();
        }
    }

    IEnumerator InitString()
    {
        yield return DefaultData.singleton.waitForEndofFrame;
        thisStringData = StringDataManager.singleton.GetString(stringIndex);
        if(thisStringData == null)
        {
            Debug.LogError(this.gameObject.name + "에서 호출한 " + stringIndex + "를 찾을 수 없습니다.");
        }
        SetString();
        yield return null;
    }

    void SetString()
    {
        switch (UserData.singleton.m_optiondata.language)
        {
            case 0:
                {
                    targetText.text = thisStringData.Lang_1;
                    break;
                }
            case 1:
                {
                    targetText.text = thisStringData.Lang_2;
                    break;
                }
            case 2:
                {
                    targetText.text = thisStringData.Lang_3;
                    break;
                }
            case 3:
                {
                    targetText.text = thisStringData.Lang_4;
                    break;
                }
            case 4:
                {
                    targetText.text = thisStringData.Lang_5;
                    break;
                }
            case 5:
                {
                    targetText.text = thisStringData.Lang_6;
                    break;
                }
        }
    }

    public void OnEvent(enum_EventType eType, Component sender, object param = null)
    {
        if(eType == enum_EventType.Change_Language)
        {
            SetString();
        }
    }
}
