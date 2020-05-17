using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            //for test
            Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            LoadData();
        }
        else
        {
            DestroyImmediate(this);
        }
        
    }
    private bool isForTestDeleteAllFile = false; // 테스트용 DeleteAllData 버튼을 누른 후 게임을 종료했을 때 마지막 씬이 저장되는 것을 방지.

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public List<string> list_completestream;
    public OptionData m_optiondata;
    public string m_nowCam;

    private void Start()
    {
        StartCoroutine(DelayInit());
        //InitData();
    }
    private void OnLevelWasLoaded(int level)
    {

                    StartCoroutine(DelayInit());
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
    IEnumerator DelayInit()
    {
        yield return new WaitForEndOfFrame();
        
        InitData();
        yield return null;
    }
    void InitData()
    {

        switch (SceneManager.GetActiveScene().name)
        {
            case "MainScene":
                {
                    if (StringDataManager.singleton.isLoaded == false) StringDataManager.singleton.LoadStringData();
                    
                    break;
                }
            case "GameScene":
                {
                    StreamDataManager.singleton.InitStream();
                    EventManager.Singleton.PostNotification(enum_EventType.Init_StreamData, this);
                    if (StringDataManager.singleton.isLoaded == false) StringDataManager.singleton.LoadStringData();
                    CameraManager.singleton.LoadCamera();
                    StreamItemManager.singleton.InitItem();
                    break;
                }
        }
    }
    void LoadData_Stream()
    {
        list_completestream = new List<string>();

        if  (ES3.KeyExists("streamdata") == false)
        {
            //if(DevDescriptionManager.singleton.m_isFortestConsoleShow) Debug.Log("streamdata에 저장된 데이터가 없습니다. 기본 값을 사용합니다.");
        }
        list_completestream = ES3.Load<List<string>>("streamdata", "userdata.es3", defaultValue: new List<string>());
    }
    void LoadData_Cam()
    {
        if (ES3.KeyExists("nowcamera") == false)
        {
            //if (DevDescriptionManager.singleton.m_isFortestConsoleShow) Debug.Log("nowcamera에 저장된 데이터가 없습니다. 기본 값을 사용합니다.");
            
        }
        m_nowCam = ES3.Load<string>("nowcamera", "userdata.es3", defaultValue: "C013001");
        
    }

    void LoadData_Option()
    {
        if (ES3.KeyExists("optiondata") == false)
        {
            //if (DevDescriptionManager.singleton.m_isFortestConsoleShow) Debug.Log("inventorydata에 저장된 데이터가 없습니다. 기본 값을 사용합니다.");
        }
        m_optiondata = ES3.Load<OptionData>("optiondata", "optiondata.es3", defaultValue: new OptionData());
    }

    void SaveData()
    {
        //for test
        if (isForTestDeleteAllFile) return; 

        SaveData_Cam();
        SaveData_Stream();
        SaveData_Option();
    }

    public void SaveData_Cam()
    {
        ES3.Save<string>("nowcamera", m_nowCam, "userdata.es3");
    }

    public void SaveData_Stream()
    {
        ES3.Save<List<string>>("streamdata", list_completestream, "userdata.es3");
    }

    public void SaveData_Option()
    {

        ES3.Save<OptionData>("optiondata", m_optiondata, "optiondata.es3");
    }

    public void DeleteAllData()
    {
        ES3.DeleteKey("nowcamera");
        ES3.DeleteKey("streamdata");
        ES3.DeleteKey("optiondata");
        ES3.DeleteFile("userdata.es3");
        ES3.DeleteFile("optiondata.es3");

        LoadData();
    }

    public void DeleteAllDataForTest()
    {
        DeleteAllData();
        isForTestDeleteAllFile = true;
    }

    public void CompleteStream(string target)
    {


        if (list_completestream.Contains(target))
        {
            Debug.LogError(string.Format($"{this.gameObject.name} 에서 실행된 {target} 은 이미 list_completestream 에 포함 된 항목"));
        }
        else
        {
            list_completestream.Add(target);
            EventManager.Singleton.PostNotification(enum_EventType.Complete_StreamData, this, target);
        }

        SaveData_Stream();


    }

    public bool IsCompleteStream(StreamData targetData)
    {

        //for test
        //Debug.Log(string.Format($"{targetData.index} 에 대한 IsCompleteStream 체크 시작"));

        for (int i = list_completestream.Count - 1; i >= 0; i--)
        {
            //for test
            //Debug.Log(string.Format($"{list_completestream[i]} 를 알아보자"));

            if (list_completestream[i] == targetData.index)
            {
                //for test
                //Debug.Log(string.Format($"{targetData.index} 가 list_completeStream[i]에 포함 된 것으로 밝혀져... 충격!"));

                return true;
            }
        }
        return false;
    }
}
