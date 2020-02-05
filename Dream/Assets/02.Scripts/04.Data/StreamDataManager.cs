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
    public StreamData initStreamOnFirstPlay;

    public void InitStream()
    {
        // 게임 시작 시 Stream 의 완료 상태를 변경
        if(list_streamData == null)
        {
            Debug.LogError(string.Format($"{this.gameObject.name} 이 null 값 입니다."));
            return;
        }
        list_streamData = new List<StreamData>();
        StreamData[] tempArr = this.GetComponentsInChildren<StreamData>();
        for (int i = 0; i < tempArr.Length; i++)
        {
            list_streamData.Add(tempArr[i]);
        }

        foreach (string s in UserData.singleton.list_completestream)
        {
            (list_streamData.Find(x => x.index == s)).InitStreamData(true);
        }




        StreamItemManager.singleton.InitItem();

        if (!initStreamOnFirstPlay.IsComplete && !UserData.singleton.list_completestream.Contains(initStreamOnFirstPlay.index))
        {
            //for test
            //Debug.Log("게임의 첫 시작이니, initStreamOnFirstPlay 발동!");
            this.initStreamOnFirstPlay.CompleteStream();
        }

    }
    public void CompleteStream(StreamData target)
    {
        
        UserData.singleton.CompleteStream(target.index);
        StreamItemManager.singleton.OnChangeStreamData();
    }

}
