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
    [SerializeField] private bool isComplete = false;
    public bool IsComplete { get { return isComplete; } }
    [Header(" + 발동에 따른 스트림 오브젝트")]
    public List<StreamObject> streamObjects;
    private int numNowObject = 0;

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
        isComplete = true;
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

        StartAction();
        /*
        StartCoroutine()

        for(int i = 0; i < streamObjects.Count; i++)
        {

        }
        foreach (var e in streamObjects)
        {
            e.DoAction();
        }
        */
    }

    void StartAction()
    {
        numNowObject = 0;
        StartCoroutine(DoAction(streamObjects[numNowObject]));
    }

    IEnumerator DoAction(StreamObject targetObject)
    {
        targetObject.DoAction();
        if (targetObject.isplayafterComplete)
        {
            while (!targetObject.IsEnd)
            {
                yield return new WaitForSecondsRealtime(0.5f * Time.deltaTime);
            }
        }

        numNowObject++;
        if (numNowObject == streamObjects.Count)
        {
            //for test
            Debug.Log("끝났으므로 반환");
            yield return null;
        }
        else
        {
            //for test
            Debug.Log(targetObject.name + "가 완료 됨! 다음으로 넘어가요! numNowObject =  " + numNowObject);
            StartCoroutine(DoAction(streamObjects[numNowObject]));
        }
    }

    public void InitStreamData(bool _isComplete = false)
    {
        if (_isComplete && streamObjects != null)
        {
            isComplete = true;
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
