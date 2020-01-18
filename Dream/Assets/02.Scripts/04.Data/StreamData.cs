using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StreamData : ScriptableObject
{
    //Save & Load 의 기준이 되는 데이터
    public string m_index = null;
    public string m_desc;
    public bool m_isComplete = false;

    public void Start()
    {
        if (m_index.Equals(null))
        {
            Debug.LogError(string.Format($"{this.name} 의 m_index 가 설정되어있지 않습니다."));
        }
        this.name = m_index;
    }
    
    public void ResetComplete()
    {
        m_isComplete = false;
    }
    public void CompleteStream()
    {
        FindObjectOfType<StreamDataManager>().CompleteStream(this);
    }

    public void LoadCompleteStream(bool isComplete)
    {
        if (isComplete)
        {
            m_isComplete = true;
        }
    }

}
