using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreamItem : MonoBehaviour
{
    [Header("+ 개발자 메모용 : 프로그램에 미포함")]
    public string m_name; // 개발자 메모용1
    public string m_desc; // 개발자 메모용2

    [Header("+ 입력해야 할 영역")]
    public string m_itemIndex;
    //public string m_streamIndex_for_gatherItem;
    //public string m_streamIndex_for_clearItem;

    public StreamData m_streamData_for_gatherItem;
    public StreamData m_streamData_for_clearItem;

    [Header("+ 아이템 아이콘")]
    public Sprite m_icon;

    [Header("+ 미입력 속성")]
    [SerializeField] private bool m_isHave = false;

    private void Awake()
    {
        CheckEmpty();
    }

    private void CheckEmpty()
    {
        if (IsEmptyString(m_itemIndex))
        {
            Debug.LogError(this.name + "의 m_indexIndex 가 입력되지 않았습니다.");
        }
        
        if (m_itemIndex == null)
        {
            Debug.LogError(this.name + "의 m_icon 이 null 입니다");
        }
    }
    private bool IsEmptyString(string target)
    {
        if(target.Equals(null) || target.Equals(""))
        {
            return true;
        }
        return false;
    }

    public void OnChangeStreamData()
    {
        if (UserData.singleton.list_completestream.Contains(m_streamData_for_gatherItem.index)
            && !(UserData.singleton.list_completestream.Contains(m_streamData_for_clearItem.index)))
        {
            m_isHave = true;
        }
        else
        {
            m_isHave = false;
        }
    }
    public void ResetComplete()
    {
        m_isHave = false;
    }
    public bool GetIsHave()
    {
        return m_isHave;
    }
    
}
