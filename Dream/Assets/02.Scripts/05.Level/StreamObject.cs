using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamObject : MonoBehaviour
{
    [Header(" + 설명")]
    public string descripiton;

    [Header(" + 대상 GameObject")]
    public List<GameObject> m_targetObject;

    [Header(" + StreamObject 행동")]
    public enum_ObjectAction m_objectAction;

    [Header(" + 초기화 시 무시되는 데이터인가?")]
    [SerializeField] private bool isIgnoreOnLoad = true;

    [Header(" + StreamObject 이 Talk 일때")]
    public List<string> dialogs;
    public List<StreamData> streamData_onEndDialog;

    public bool IsIgnoreOnLoad
    {
        get
        {
            return isIgnoreOnLoad;
        }
    }
    public void Start()
    {
        /*
        if (!CheckValid())
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "CheckValid");
            return;
        }
        */
        CheckIsIgnoreOnLoad();
    }
    /*
    private bool CheckValid()
    {
        if (m_targetObject == null)
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "m_targetObject");
            return false;
        }
        if(m_objectAction == null)
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "m_objectAction");
            return false;
        }

        return true;
    }
    */
    public void DoAction()
    {

        switch (m_objectAction)
        {
            case enum_ObjectAction.Defalut:
                {
                    Debug.LogError(this.gameObject.name + "의 m_objectAction 이 설정되어있지 않다 (default)");
                    break;
                }
            case enum_ObjectAction.Hide:
                {
                    ObjectAction_Hide();
                    break;
                }

            case enum_ObjectAction.Show:
                {
                    ObjectAction_Show();
                    break;
                }

            case enum_ObjectAction.Init:
                {
                    ObjectAction_Init();
                    break;
                }

            case enum_ObjectAction.Talk:
                {
                    ObjectAction_Talk();
                    break;
                }
                
        }
    }
    private void ObjectAction_Talk()
    {
        Dialog.singleton.ShowDialog(this);
    }

    public void OjbectAction_Talk_onComplete()
    {
        if(streamData_onEndDialog != null)
        {
            foreach(var e in streamData_onEndDialog)
            {
                //for test
                Debug.Log("talk 완료에 따라 " + e.index + " 의 complete 발동!");
                e.CompleteStream();
            }
        }
    }

    private void ObjectAction_Show()
    {
        foreach(var e in m_targetObject)
        {
            e.SetActive(true);
        }
    }

    private void ObjectAction_Hide()
    {
        foreach (var e in m_targetObject)
        {
            e.SetActive(false);
        }
    }

    private void ObjectAction_Init()
    {
        //for test
        Debug.Log("Init 을 구현해야 합니다! - 게임을 처음 켰을 때 Load 된 정보에 따라서 해당 프랍을 show / hide / move ... 시켜야 합니다");


    }



    private void CheckIsIgnoreOnLoad()
    {
        //for test
        if (DevDescriptionManager.singleton.m_isFortestConsoleShow) Debug.Log(string.Format($"enum_ObjectAction 이 50 보다 작으면 Load 시에 Ignore - targetAction = {(int)m_objectAction}"));

        //enum_ObjectAction 의 규칙 : 50 보다 작으면 Ignore 하는 데이터

        isIgnoreOnLoad = (int)m_objectAction < 50 ? true : false;

    }
}
