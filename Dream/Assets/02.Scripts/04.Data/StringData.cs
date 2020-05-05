using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringData
{
    public string Index;
    public string Lang_1;
    public string Lang_2;
    public string Lang_3;
    public string Lang_4;
    public string Lang_5;
    public string Lang_6;
    public string PlaySoundEffect;
    public string FontColor;

    public StringData(string index, string lang_1, string lang_2, string lang_3, string lang_4, string lang_5, string lang_6, string soundEffect, string color)
    {
        Index = index;
        Lang_1 = ReplaceTag(lang_1);
        Lang_2 = ReplaceTag(lang_2);
        Lang_3 = ReplaceTag(lang_3);
        Lang_4 = ReplaceTag(lang_4);
        Lang_5 = ReplaceTag(lang_5);
        Lang_6 = ReplaceTag(lang_6);

        PlaySoundEffect = soundEffect;
        FontColor = color;
    }

    private string ReplaceTag(string target)
    {
        //타겟 스트링에 대해 기존 Tag 를 실제 적용되는 스트링으로 변환하는 작업을 한다.
        string tempString = target;
        tempString.Replace("\\n", "\n");
        //for test
        Debug.Log(tempString);
        return tempString;
    }
}
