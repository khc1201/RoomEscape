using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QInput : MonoBehaviour
{

    [Header("+ 버튼 클릭 시 입력되어야 하는 항목")]
    public string input;

    [Header("+ 버튼 클릭 시 발동할 reactObject 목록들")]
    [SerializeField] private List<ReactObject> reactObjects;
    private bool hasReactObject = false;

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

        ReactObject tempObject = GetComponent<ReactObject>();
        if(tempObject == null)
        {
            Debug.Log(this.gameObject.name + "의 ReactObject 는 없음.");
        }
        else
        {
            ReactObject[] tempreactObjects = GetComponents<ReactObject>();
            reactObjects = new List<ReactObject>();
            for (int i = 0; i < tempreactObjects.Length; i++)
            {
                ReactObject temp = tempreactObjects[i];
                if (temp.isStreamObject) Debug.LogError(temp.name + "의 isStreamObject 의 값이 true 이다. 어디에서 체크 된 것인지 확인 필요.");
                temp.isQInputObject = true;
                reactObjects.Add(temp);
            }
            hasReactObject = true;
        }
        
    }

    public void OnButtonClick()
    {
        qParent.OnInput(childIndex: index, childInput: input);
        if (hasReactObject)
        {
            for(int i = 0; i < reactObjects.Count; i++)
            {
                //for test
                Debug.Log("이 부분을 StreamObject 와 같은 내용으로 수정해야 한다. ReactObject 는 이제 더이상 Tweening 만 하지 않는다!");
                reactObjects[i].OnReact();
            }
        }
    }

}
