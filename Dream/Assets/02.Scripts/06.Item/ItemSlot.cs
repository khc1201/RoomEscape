using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private GameObject m_backGround;
    [SerializeField] private Button m_button;
    [SerializeField] private Image m_icon;
    [SerializeField] private Inventory m_inventory;
    [SerializeField] private StreamItem m_itemData;

    public void Start()
    {
        InitButton();
    }

    public void InitButton()
    {
        m_backGround = this.transform.Find("BackGround").gameObject;

        m_button = this.transform.GetComponentInChildren<Button>();
        m_button.onClick.RemoveAllListeners();
        m_button.onClick.AddListener(OnClickButton);

        m_icon = this.transform.Find("Image").GetComponent<Image>();

        m_inventory = FindObjectOfType<Inventory>();
        
    }
    public bool IsExistItemData()
    {
        return m_itemData == null ? false : true;
    }
    public void OnClickButton()
    {
        m_inventory.OnSelectItem(this);
    }
    public void OnSelectItem()
    {
        if (m_backGround.gameObject.activeInHierarchy || m_itemData == null) return;
        
        m_backGround.SetActive(true);
    }
    public void OnDisselectItem()
    {
        if (!m_backGround.gameObject.activeInHierarchy) return;

        m_backGround.SetActive(false);
    }
    public void AddItem(StreamItem target)
    {
        if (target.Equals(null))
        {
            DeleteItem();
            return;
        }
        m_itemData = target;
        m_icon.enabled = true;
        m_icon.sprite = m_itemData.m_icon;
    }
    public void DeleteItem()
    {
        m_icon.enabled = false;
        m_itemData = null;
    }
}
