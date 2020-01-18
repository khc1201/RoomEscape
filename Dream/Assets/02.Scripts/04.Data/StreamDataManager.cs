using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamDataManager : MonoBehaviour
{
    public static StreamDataManager singleton;
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
        }
    }

    public List<StreamData> list_streamData;
    public void Start()
    {
        
    }
    public void InitStream()
    {
        // 게임 시작 시 Stream 의 완료 상태를 변경
        if(list_streamData == null)
        {
            Debug.LogError(string.Format($"{this.gameObject.name} 이 null 값 입니다."));
            return;
        }
        foreach(string s in UserData.singleton.list_completestream)
        {
            (list_streamData.Find(x => x.m_index == s)).m_isComplete = true;
        }

        StreamItemManager.singleton.InitItem();
    }
    public void CompleteStream(StreamData target)
    {
        
        UserData.singleton.CompleteStream(target.m_index);

        
        StreamItemManager.singleton.OnChangeStreamData();

        target.m_isComplete = true;
    }
}
