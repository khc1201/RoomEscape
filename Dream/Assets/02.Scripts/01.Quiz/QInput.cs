using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QInput : MonoBehaviour
{
    [Header("+ Number 일 경우 - 버튼 클릭 시 입력되어야 하는 항목")]
    public string input;

    [Header("+ Match 일 경우 - 클릭마다 바뀌어야 하는 번호")]
    public List<string> inputList;
    [Header("+ 초기화된 지금은 몇 번째?")]
    public int nowIdx; 

    [Header("+ 버튼 클릭 시 발동할 reactObject 목록들")]
    [SerializeField] private List<ReactObject> reactObjects;
    private bool hasReactObject = false;
    public bool isSequencePlay = false;

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

        if (qParent.answerType == enum_AnswerType.Match)
        {
            // 값에 맞게 초기화
            //qParent.OnInput(childIndex: index, childInput: inputList[nowIdx]);
            qParent.OnInitMatch(childIndex: index, childInput: inputList[nowIdx]);
        }

        qButton = this.transform.GetComponentInChildren<Button>();

        if (qButton != null)
        {
            qButton.onClick.RemoveAllListeners();
            qButton.onClick.AddListener(OnButtonClick);

        }

        ReactObject tempObject = GetComponent<ReactObject>();
        if(tempObject == null)
        {
            //Debug.Log(this.gameObject.name + "의 ReactObject 는 없음.");
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

    public void DoReact()
    {
        for (int i = 0; i < reactObjects.Count; i++)
        {
            //for test
            //Debug.Log("이 부분을 StreamObject 와 같은 내용으로 수정해야 한다. ReactObject 는 이제 더이상 Tweening 만 하지 않는다!");

            //for test
            Debug.Log(string.Format($"answerType = {qParent.answerType} // isReactObjectDontHaveItem = {qParent.isReactObjectDontHaveItem}"));

            reactObjects[i].DoAction(_isplayafterComplete: isSequencePlay, _targetButton: qButton);
        }
    }

    public void OnButtonClick()
    {
        if (qParent.answerType == enum_AnswerType.Match)
        {
            ++nowIdx;
            if(nowIdx >= inputList.Count)
            {
                nowIdx = 0;
            }
            qParent.OnInput(childIndex: index, childInput: inputList[nowIdx]);
        }
        else
        {
            qParent.OnInput(childIndex: index, childInput: input);
            
        }

        if(qParent.answerType == enum_AnswerType.CheckItem)
        {
            return;
        }

        if (hasReactObject)
        {
            

            DoReact();
            
        }
        /*
        if(qParent.answerType == enum_AnswerType.CheckItem)
        {
            qParent.UseItem();
        }
        */
    }
}
