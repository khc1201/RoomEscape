using System.Collections;
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

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public List<string> list_completestream;
    public OptionData m_optiondata;
    public string m_nowCam;

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
            /*
            string jsonString = ES3.Load<string>("streamdata");

            JsonData json = JsonMapper.ToObject(jsonString);

            for (int i = 0; i < json.Count; i++)
            {

                JsonData item = json[i];
                if (item.ToString() != "") list_completestream.Add(item.ToString());
            }
            */
            list_completestream = ES3.Load<List<string>>("streamdata");

            //for test
            Debug.Log("LoadData_Stream 완료!");
        }

    }
    void LoadData_Cam()
    {
        if (ES3.KeyExists("nowcamera") == false)
        {
            Debug.Log("nowcamera에 저장된 데이터가 없습니다. 기본 값을 사용합니다.");
            m_nowCam = "C000001";
            return;
        }
        else {
            m_nowCam = ES3.Load<string>("nowcamera");
            
            //for test
            Debug.Log("LoadData_Cam 완료! - " + m_nowCam);
        }
    }

    void LoadData_Option()
    {
        m_optiondata = new OptionData();
        if (ES3.KeyExists("optiondata") == false)
        {
            Debug.Log("inventorydata에 저장된 데이터가 없습니다. 기본 값을 사용합니다.");
            return;
        }
        else
        {
            /*
            string jsonString = ES3.Load<string>("optiondata");

            JsonData json = JsonMapper.ToObject(jsonString);
            for (int i = 0; i < json.Count; i++)
            {
                JsonData item = json[i];

                int language = int.Parse(item["language"].ToString());
                float sound = float.Parse(item["sound"].ToString());
                bool isrighthand = bool.Parse(item["isrighthand"].ToString());

                m_optiondata.ChangeOption(language, sound, isrighthand);
            }
            */
            m_optiondata = ES3.Load<OptionData>("optiondata");
            //for test
            Debug.Log("LoadData_Option 완료!");
        }
    }

    void SaveData()
    {
        SaveData_Cam();
        SaveData_Stream();
        SaveData_Option();
    }

    void SaveData_Cam()
    {

        //for test
        Debug.Log("Save Step 1");
        ES3.Save<string>("nowcamera", m_nowCam);
    }

    void SaveData_Stream()
    {

        //for test
        Debug.Log("Save Step 2");
        /*
        JsonData json_stream = JsonMapper.ToJson(list_completestream);
        ES3.Save<JsonData>(json_stream.ToString(), "streamdata");
        */
        ES3.Save<List<string>>("streamdata", list_completestream);
    }

    void SaveData_Option()
    {

        //for test
        Debug.Log("Save Step 3");
        /*
        JsonData json_option = JsonMapper.ToJson(m_optiondata);
        ES3.Save<JsonData>(json_option.ToString(), "optiondata");
        */
        ES3.Save<OptionData>("optiondata", m_optiondata);
    }

    void DeleteAllData()
    {
        ES3.DeleteKey("nowcamera");
        ES3.DeleteKey("streamdata");
        ES3.DeleteKey("optiondata");
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
