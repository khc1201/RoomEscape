using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class StringDataManager : MonoBehaviour
{
    public static StringDataManager singleton;
    private void Awake()
    {
        if (singleton == null)
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

    [SerializeField] private List<StringData> stringDatas;
    private string datapath = "02.Json/String";
    public bool isLoaded = false;
    public void LoadStringData()
    {
        TextAsset stringTextAsset = Resources.Load(datapath) as TextAsset;
        JsonData stringJson = JsonMapper.ToObject(stringTextAsset.text);
        
        for(int i = 0; i < stringJson.Count; i++)
        {
            JsonData item = stringJson[i];
            if (item["Index"].ToString() != "//")
            {
                stringDatas.Add
                    (
                        new StringData
                        (
                            index: item["Index"].ToString(),
                            lang_1: item["Lang_1"].ToString(),
                            lang_2: item["Lang_2"].ToString(),
                            lang_3: item["Lang_3"].ToString(),
                            lang_4: item["Lang_4"].ToString(),
                            lang_5: item["Lang_5"].ToString(),
                            lang_6: item["Lang_6"].ToString(),
                            soundEffect: item["PlaySoundEffect"].ToString(),
                            color:item["FontColor"].ToString()
                        )
                    );
            }
        }
        isLoaded = true;
    }
    public string GetText(string index)
    {
        StringData target = stringDatas.Find(x => x.Index == index);
        string tempString = "?키없음_" + index;
        if (target == null)
        {
            Debug.LogError(index + "의 키가 존재하지 않음");
            return tempString;
        }

        switch (UserData.singleton.m_optiondata.language)
        {
            case 0:
                {
                    tempString = target.Lang_1;
                    break;
                }
            case 1:
                {
                    tempString = target.Lang_2;
                    break;
                }
            case 2:
                {
                    tempString = target.Lang_3;
                    break;
                }
            case 3:
                {
                    tempString = target.Lang_4;
                    break;
                }
            case 4:
                {
                    tempString = target.Lang_5;
                    break;
                }
            case 5:
                {
                    tempString = target.Lang_6;
                    break;
                }
        }
        return tempString;

    }
    public StringData GetString(string index)
    {
        StringData target = stringDatas.Find(x => x.Index == index);
        if (target == null)
        {
            Debug.LogError(index + "의 키가 존재하지 않음");
            return new StringData("nullError", "?키없음_" + index, "?키없음_" + index, "?키없음_" + index, "?키없음_" + index, "?키없음_" + index, "?키없음_" + index, "?키없음_" + index, "?키없음_" + index);
        }
        return target;
    }
}
