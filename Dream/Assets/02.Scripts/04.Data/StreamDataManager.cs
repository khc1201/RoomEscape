using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamDataManager : MonoBehaviour
{
    public static StreamDataManager singleton;
    public GameObject m_streamParent;
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
        InitList();        
    }
    public void InitList()
    {
        if(m_streamParent == null)
        {
            Debug.LogError(string.Format($"{this.gameObject.name} 이 null 값 입니다."));
        }
        list_streamData = new List<StreamData>();
        foreach(var e in m_streamParent.GetComponentsInChildren<StreamData>())
        {
            list_streamData.Add(e);
        }
    }
    public void CompleteStream(StreamData target)
    {
        FindObjectOfType<UserData>().CompleteStream(target.m_index);
    }
}
