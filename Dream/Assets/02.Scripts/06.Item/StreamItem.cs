using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreamItem : MonoBehaviour
{
    [Header("+ 개발자 메모용 : 프로그램에 미포함")]
    public string _name; // 개발자 메모용1
    public string _desc; // 개발자 메모용2

    [Header("+ 입력해야 할 영역")]
    public string itemIndex;
    //public string streamIndex_for_gatherItem;
    //public string streamIndex_for_clearItem;

    public StreamData streamData_for_gatherItem;
    public StreamData streamData_for_clearItem;

    [Header("+ 아이템 아이콘")]
    public Sprite icon;

    [Header("+ 미입력 속성")]
    [SerializeField] private bool isHave = false;

    private void Awake()
    {
        CheckEmpty();
    }

    private void CheckEmpty()
    {
        if (IsEmptyString(itemIndex))
        {
            Debug.LogError(this.name + "의 indexIndex 가 입력되지 않았습니다.");
        }
        
        if (itemIndex == null)
        {
            Debug.LogError(this.name + "의 icon 이 null 입니다");
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
        if (UserData.singleton.list_completestream.Contains(streamData_for_gatherItem.index)
            && !(UserData.singleton.list_completestream.Contains(streamData_for_clearItem.index)))
        {
            isHave = true;
        }
        else
        {
            isHave = false;
        }
    }
    public void ResetComplete()
    {
        isHave = false;
    }
    public bool GetIsHave()
    {
        return isHave;
    }
    
}
