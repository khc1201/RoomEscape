using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionChanger : MonoBehaviour
{
    public Text lanText;
    public Button lanRightButton;
    public Button lanLeftButton;
    public Slider soundSlider;
    private string[] langTexts = { "Str_Language_KOR", "Str_Language_ENG", "Str_Language_CHN", "Str_Language_JPN", "Str_Language_ESP", "Str_Language_6" };
    OptionData optionData;

    private void OnEnable()
    {
        if(optionData == null) optionData = UserData.singleton.m_optiondata;

        soundSlider.value = optionData.sound;

        SetLanText();
        
    }


    public void OnSlideValudChange()
    {
        optionData.sound = soundSlider.value;
    }

    public void OnButtonClick(bool isRight)
    {
        if (isRight)
        {
            if(UserData.singleton.m_optiondata.language + 1 >= DefaultData.singleton.LanguageCount)
            {
                UserData.singleton.m_optiondata.language = 0;
            }
            else
            {
                ++ UserData.singleton.m_optiondata.language;
            }
        }
        else
        {
            if(UserData.singleton.m_optiondata.language == 0)
            {
                UserData.singleton.m_optiondata.language = DefaultData.singleton.LanguageCount - 1;
            }
            else
            {
                --UserData.singleton.m_optiondata.language;
            }
        }

        EventManager.Singleton.PostNotification(enum_EventType.Change_Language, this);
        SetLanText();
    }


    void SetLanText()
    {
        switch (UserData.singleton.m_optiondata.language)
        {
            case 0:
                {
                    lanText.text = StringDataManager.singleton.GetString(langTexts[0]).Lang_1;
                    break;
                }
            case 1:
                {
                    lanText.text = StringDataManager.singleton.GetString(langTexts[1]).Lang_1;
                    break;
                }
            case 2:
                {
                    lanText.text = StringDataManager.singleton.GetString(langTexts[2]).Lang_1;
                    break;
                }
            case 3:
                {
                    lanText.text = StringDataManager.singleton.GetString(langTexts[3]).Lang_1;
                    break;
                }
            case 4:
                {
                    lanText.text = StringDataManager.singleton.GetString(langTexts[4]).Lang_1;
                    break;
                }
            case 5:
                {
                    lanText.text = StringDataManager.singleton.GetString(langTexts[5]).Lang_1;
                    break;
                }
        }
    }
}
