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
    private int numNowObject;
    private WaitForSeconds waitTimeTick;
    public bool isCompleteOnStartAction = false;

    public void Start()
    {
        if (index.Equals(null))
        {
            Debug.LogError(string.Format($"{this.name} 의 m_index 가 설정되어있지 않습니다."));
        }
        this.gameObject.name = index;
        GetStreamObject();

        waitTimeTick = new WaitForSeconds(0.5f * Time.deltaTime);
    }

    void GetStreamObject()
    {
        StreamObject[] tempArr = this.GetComponentsInChildren<StreamObject>();
        streamObjects = new List<StreamObject>();
        for (int i = 0; i < tempArr.Length; i++)
        {
            streamObjects.Add(tempArr[i]);
        }
    }

    public void CompleteStream()
    {
        isComplete = true;
        StartAction();
    }

    private void CompleteAction()
    {
        StreamDataManager.singleton.CompleteStream(this);
    }

    void StartAction()
    {
        if (streamObjects == null)
        {
            Debug.Log("streamObjects 가 null 로 비어있다.");
            return;
        }

        numNowObject = 0;
        StreamDataManager.singleton.nowPlayingStream = this;

        if (isCompleteOnStartAction)
        {
            CompleteAction();
        }

        StartCoroutine(DoAction(streamObjects[numNowObject]));
    }
    
    IEnumerator DoAction(StreamObject targetObject)
    {
        targetObject. DoAction();
        ++numNowObject;

        if (targetObject.isplayafterComplete)
        {
            while (!targetObject.IsEnd)
            {
                yield return waitTimeTick;
            }
        }

       
        //for test
        //Debug.Log(string.Format(numNowObject+ "번째 DoAction // streamObjects의 count 는 " + streamObjects.Count));
        if (numNowObject == streamObjects.Count)
        {
            //for test
            //Debug.Log("끝났으므로 반환");
            if (!isCompleteOnStartAction) CompleteAction();                
            StreamDataManager.singleton.nowPlayingStream = null;
            //CompleteAction();
            yield return null;
        }
        else
        {
            //for test
            //Debug.Log(targetObject.name + "가 완료 됨! 다음으로 넘어가요! numNowObject =  " + numNowObject);
            StartCoroutine(DoAction(streamObjects[numNowObject]));
        }
    }

    /*
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
    */

}
