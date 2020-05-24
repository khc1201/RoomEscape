using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamItemManager : MonoBehaviour
{
    public static StreamItemManager singleton;
    public Inventory m_inventory;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    [Header("+ 모든 아이템의 목록")]
    public List<StreamItem> list_streamItem;

    public void InitItem()
    {
        OnChangeStreamData();
    }

    //각 아이템들의 ishave 를 바꿔줌
    public void OnChangeStreamData()
    {

        if (m_inventory == null)
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "m_inventory");
            return;
        }
        if(list_streamItem == null)
        {
            DevDescriptionManager.singleton.LogNullError(this.gameObject, "list_streamItem");
            return;
        }
        
        for (int i = 0; i < list_streamItem.Count; i++)
        {
            list_streamItem[i].OnChangeStreamData();
        }
        m_inventory.RefreshNowItemList();
        Hint.HintManager.singleton.OnChangeStreamData();
    }
}
