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
        Lang_1 = lang_1;
        Lang_2 = lang_2;
        Lang_3 = lang_3;
        Lang_4 = lang_4;
        Lang_5 = lang_5;
        Lang_6 = lang_6;
        PlaySoundEffect = soundEffect;
        FontColor = color;
    }
}
