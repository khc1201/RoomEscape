using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private Inventory inventory;
    public StreamItem itemData;

    public void Start()
    {
        InitButton();
    }

    public void InitButton()
    {
        backGround = this.transform.Find("BackGround").gameObject;

        button = this.transform.GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClickButton);

        icon = this.transform.Find("Image").GetComponent<Image>();

        inventory = FindObjectOfType<Inventory>();
        
    }
    public bool IsExistItemData()
    {
        return itemData == null ? false : true;
    }
    public void OnClickButton()
    {
        inventory.OnSelectItem(this);
    }
    public void OnSelectItem()
    {
        if (backGround.gameObject.activeInHierarchy || itemData == null) return;
        
        backGround.SetActive(true);
    }
    public void OnDisselectItem()
    {
        if (!backGround.gameObject.activeInHierarchy) return;

        backGround.SetActive(false);
    }
    public void AddItem(StreamItem target)
    {
        if (target.Equals(null))
        {
            DeleteItem();
            return;
        }
        itemData = target;
        icon.enabled = true;
        icon.sprite = itemData.icon;
    }
    public void DeleteItem()
    {
        icon.enabled = false;
        backGround.SetActive(false);
        itemData = null;
    }
}
