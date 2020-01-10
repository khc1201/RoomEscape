﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class UserData : MonoBehaviour
{
    public static UserData singleton;
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
        }
    }

    public List<string> list_completestream;
    public OptionData m_optiondata;
    public string m_nowCam = "C000001";

    private void Start()
    {
        LoadData();
    }
    
    public void SetNowCam(string target)
    {
        m_nowCam = target;
        SaveData_Cam();
    }

    void LoadData()
    {
        LoadData_Stream();
        LoadData_Option();
        LoadData_Cam();
    }
    void LoadData_Stream()
    {
        list_completestream = new List<string>();

        if  (ES3.KeyExists("streamdata") == false)
        {
            Debug.Log("streamdata에 저장된 데이터가 없습니다. 기본 값을 사용합니다.");
            return;
        }
        else
        {
            string jsonString = ES3.Load<string>("streamdata");

            JsonData json = JsonMapper.ToObject(jsonString);

            for (int i = 0; i < json.Count; i++)
            {

                JsonData item = json[i];
                if (item.ToString() != "") list_completestream.Add(item.ToString());
            }
        }

    }
    void LoadData_Cam()
    {
        if (ES3.KeyExists("nowcamera") == false)
        {
            Debug.Log("nowcamera에 저장된 데이터가 없습니다. 기본 값을 사용합니다.");
            return;
        }

        m_nowCam = ES3.Load<string>("nowcamera");
    }

    void LoadData_Option()
    {
        m_optiondata = new OptionData();
        if (ES3.KeyExists("inventorydata") == false)
        {
            Debug.Log("inventorydata에 저장된 데이터가 없습니다. 기본 값을 사용합니다.");
            return;
        }
        else
        {
            string jsonString = ES3.Load<string>("inventorydata");

            JsonData json = JsonMapper.ToObject(jsonString);
            for (int i = 0; i < json.Count; i++)
            {
                JsonData item = json[i];

                int language = int.Parse(item["language"].ToString());
                float sound = float.Parse(item["sound"].ToString());
                bool isrighthand = bool.Parse(item["isrighthand"].ToString());

                m_optiondata.ChangeOption(language, sound, isrighthand);
            }
        }
    }

    void SaveData()
    {
        SaveData_Stream();
        SaveData_Option();
        SaveData_Cam();
    }

    void SaveData_Cam()
    {
        ES3.Save<string>(m_nowCam, "nowcamera");
    }

    void SaveData_Stream()
    {
        JsonData json_stream = JsonMapper.ToJson(list_completestream);
        ES3.Save<JsonData>(json_stream.ToString(), "streamdata");
    }

    void SaveData_Option()
    {
        JsonData json_option = JsonMapper.ToJson(m_optiondata);
        ES3.Save<JsonData>(json_option.ToString(), "optiondata");
    }

    public void CompleteStream(string target)
    {
        if (list_completestream.Contains(target))
        {
            Debug.LogError(string.Format($"{this.gameObject.name} 에서 실행된 {target} 은 이미 list_completestream 에 포함 된 항목"));
        }
        else list_completestream.Add(target);

        SaveData_Stream();
    }
}