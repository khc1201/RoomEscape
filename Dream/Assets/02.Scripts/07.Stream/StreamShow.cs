using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreamShow : MonoBehaviour, IListener
{
    public bool isStreamShowObject = false;
    public enum_ShowHide defaultOnLoad = enum_ShowHide.SHOW;
    public GameObject targetObject;
    public QParent targetButton;
    //public StreamData streamData_for_ShowObject;
    //public StreamData streamData_for_HideObject;
    public List<StreamShowList> showList;
    string targetStream;

    private void Start()
    {
        if (isStreamShowObject)
        {
            if (targetObject == null && targetButton == null)
            {
                //for test
                Debug.LogError(this.gameObject.name + "의 target 이 아무것도 정해지지 않았습니다");
            }
            EventManager.Singleton.AddListener(enum_EventType.Complete_StreamData, this);
            EventManager.Singleton.AddListener(enum_EventType.Init_StreamData, this);
            SetObjectVisible(defaultOnLoad);
        }
    }
    // Start is called before the first frame update

    
    private void SetObjectVisible(enum_ShowHide showhide)
    {
        if(showhide == enum_ShowHide.SHOW)
        {
            targetObject.SetActive(true);
        }
        else
        {
            targetObject.SetActive(false);
        }
        
    }

    public void OnEvent(enum_EventType etype, Component sender, object param = null)
    {
        if (etype == enum_EventType.Complete_StreamData)
        {
            
            if(param != null)
            {
                targetStream = param as string;
                if(targetStream == null)
                {
                    Debug.LogError(this.gameObject.name + "에서 OnEvent 를 실행 도중 param 이 null 이어서 실행 실패");
                    return;
                }

                //for test
                Debug.Log("targetStream = " + targetStream);

                for (int i = showList.Count - 1 ; i >= 0; i--)
                {
                    if (targetStream == showList[i].forStreamdata.index)
                    {
                        SetObjectVisible(showList[i].showhide);
                        return;
                    }
                }
                
            }   
        }
        else if(etype == enum_EventType.Init_StreamData)
        {
            List<string> tempArr = UserData.singleton.list_completestream;
            for(int i = tempArr.Count - 1; i >= 0; i--)
            {
                for(int j = showList.Count - 1; j >= 0; j--)
                {
                    if(tempArr[i] == showList[j].forStreamdata.index)
                    {
                        SetObjectVisible(showList[j].showhide);
                        //for test
                        Debug.Log("Init 완료 - UserData 인 " + tempArr[i] + "와 비교한 값은 showList[" + j + "]입니다. 그 값은 : " + showList[j].forStreamdata.index) ;
                            return;
                    }
                }
            }
        }
        
    }
}
