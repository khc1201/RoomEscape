using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamData : MonoBehaviour
{
    //Save & Load 의 기준이 되는 데이터
    [Header(" + 정보")]
    public string index = null;
    public string desc;
    [Header(" + 완료 되었는지?")]
    public bool isComplete = false;
    [Header(" + 발동에 따른 스트림 오브젝트")]
    public List<StreamObject> streamObjects;

    public void Start()
    {
        if (index.Equals(null))
        {
            Debug.LogError(string.Format($"{this.name} 의 m_index 가 설정되어있지 않습니다."));
        }
        this.gameObject.name = index;
    }

    public void CompleteStream()
    {
        StreamDataManager.singleton.CompleteStream(this);
        CompleteAction();
    }

    private void CompleteAction()
    {
        if (streamObjects == null)
        {
            Debug.Log("streamObjects 가 null 로 비어있다.");
            return;
        }

        foreach (var e in streamObjects)
        {
            e.DoAction();
        }
    }

    public void InitStreamData(bool _isComplete = false)
    {
        if (_isComplete && streamObjects != null)
        {
            for(int i = 0; i < streamObjects.Count; i++)
            {
                if (!(streamObjects[i].IsIgnoreOnLoad))
                {
                    streamObjects[i].DoAction();
                }
            }
        }
    }

}
