using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemSlot m_slot_1;
    public ItemSlot m_slot_2;
    public ItemSlot m_slot_3;
    public ItemSlot m_slot_4;
    public ItemSlot m_slot_5;

    [SerializeField] List<ItemSlot> list_itemslots;
    [Header("+ 모든 아이템의 목록")]
    public List<StreamItem> list_streamItem;
    

    public ItemSlot m_nowSelectedItem;

    public void Start()
    {
        InitInventory();
    }

    private void InitInventory()
    {
        m_slot_1 = transform.Find("Slots").transform.Find("Slots (1)").GetComponent<ItemSlot>();
        m_slot_2 = transform.Find("Slots").transform.Find("Slots (2)").GetComponent<ItemSlot>();
        m_slot_3 = transform.Find("Slots").transform.Find("Slots (3)").GetComponent<ItemSlot>();
        m_slot_4 = transform.Find("Slots").transform.Find("Slots (4)").GetComponent<ItemSlot>();
        m_slot_5 = transform.Find("Slots").transform.Find("Slots (5)").GetComponent<ItemSlot>();


        list_itemslots = new List<ItemSlot>();

        list_itemslots.Add(m_slot_1);
        list_itemslots.Add(m_slot_2);
        list_itemslots.Add(m_slot_3);
        list_itemslots.Add(m_slot_4);
        list_itemslots.Add(m_slot_5);
        
        foreach(var e in list_itemslots)
        {
            e.OnDisselectItem();
        }
    }
    private void LoadItemData()
    {
        //완료된 streamdata 에 따라서 각 아이템을 로드 완료
    }

    public void OnSelectItem(ItemSlot target)
    {
        for (int i = 0; i < list_itemslots.Count; i++)
        {
            list_itemslots[i].OnDisselectItem();
        }

        if (!target.IsExistItemData())
        {
            if(DevDescriptionManager.singleton.m_isFortestConsoleShow) Debug.Log(target.name + " 에 Item Data 가 존재하지 않습니다. 아무것도 선택하지 않습니다.");
            return;
        }

        m_nowSelectedItem = target;
        
        m_nowSelectedItem.OnSelectItem();
    }
}
