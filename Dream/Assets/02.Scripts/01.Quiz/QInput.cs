using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QInput : MonoBehaviour
{

    [Header("+ 버튼 클릭 시 입력되어야 하는 항목")]
    public string input;

    [Header("+ 버튼 클릭 시 발동할 reactObject")]
    public bool hasReactObject = false;
    private List<ReactObject> reactObjects;

    [Header("+ 프로퍼티 (미입력 항목)")]
    [SerializeField] int index;
    private QParent qParent;
    private Button qButton;

    private void Start()
    {
        if(input == null)
        {
            Debug.LogError(this.gameObject.name + "의 input string 이 null 이다.");
            return;
        }
        InitProperties();
    }

    private void InitProperties()
    {

        index = this.gameObject.transform.GetSiblingIndex();
        qParent = this.transform.GetComponentInParent<QParent>();

        qButton = this.transform.GetComponentInChildren<Button>();
        if(qButton != null)
        {
            qButton.onClick.RemoveAllListeners();
            qButton.onClick.AddListener(OnButtonClick);
        }


        if (this.GetComponent<ReactObject>() != null && !hasReactObject)
        {
            Debug.LogError(this.gameObject.name + "의 ReactObject 는 존재하는데, hasReactObject 가 false 로 설정됨.");
        }

        if (hasReactObject)
        {
            reactObjects = new List<ReactObject>(); 
            if(this.GetComponent<ReactObject>() == null)
            {
                Debug.LogError(this.gameObject.name + "의 hasReactObject 는 true 인데, 정작 ReactObject 가 없어서 초기화 실패");
                return;
            }
            foreach(var e in this.GetComponents<ReactObject>())
            {
                reactObjects.Add(e);
            }
        }
    }

    public void OnButtonClick()
    {
        qParent.OnInput(childIndex: index, childInput: input);
        if (hasReactObject)
        {
            for(int i = 0; i < reactObjects.Count; i++)
            {
                reactObjects[i].OnReact();
            }
        }
    }

}
