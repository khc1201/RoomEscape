using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamObject : MonoBehaviour, IListener
{
    [Header(" + StreamData 조건")]
    public StreamData m_streamdata_target;

    [Header(" + 대상 GameObject")]
    public List<GameObject> m_targetObject;

    [Header(" + StreamObject 행동")]
    public enum_ObjectAction m_objectAction;

    public void Start()
    {
        if (!CheckValid())
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "CheckValid");
            return;
        }
        AddEventListener();
    }
    private bool CheckValid()
    {
        if (m_targetObject == null)
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "m_targetObject");
            return false;
        }
        if(m_streamdata_target == null)
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "m_streamdata_target");
            return false;
        }
        if(m_objectAction == null)
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "m_objectAction");
            return false;
        }

        return true;
    }
    private void AddEventListener()
    {
        EventManager.Singleton.AddListener(EVENT_TYPE.Complete_StreamData, this);
    }

    public void OnEvent(EVENT_TYPE etype, Component sender, object param = null)
    {
        StreamData tempData = param as StreamData;
        if(tempData == null)
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "OnEvent - param");
        }
        if(etype.Equals(EVENT_TYPE.Complete_StreamData) && tempData.m_index == m_streamdata_target.m_index)
        {
            //for test
            Debug.Log("이곳은 스트림 데이터가 갱신되는 찰나의 순간에 대한 이벤트 정보일 뿐입니다. Init 가 필요합니다.");
            switch (m_objectAction)
            {
                case enum_ObjectAction.Show:
                    {
                        ObjectAction_Show();
                        break;
                    }
                case enum_ObjectAction.Hide:
                    {
                        ObjectAction_Hide();
                        break;
                    }
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


}
