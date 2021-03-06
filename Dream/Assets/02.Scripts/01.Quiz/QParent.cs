﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QParent : MonoBehaviour
{
    [Header("+ 정답의 타입")]
    public enum_AnswerType answerType = enum_AnswerType.Default;

    [Header("+ 정답 입력")]
    private QChild qChild;
    public string[] answer;
    public string answerString;
    private string defaultAnswer = "*";
    public Text answerText;

    [Header("+ 정답 시 발동할 StreamData")]
    public bool doNotifyOnAnswer_StreamData = false;
    public List<StreamData> onAnswerStreamDatas;

    [Header("+ 현재 입력된 값 (미입력 영역)")]
    [SerializeField] string[] nowInput;

    [Header("+ 체크할 아이템")]
    public StreamItem checktargetItem;

    [Header("++ 체크할 아이템이 없다면 reactObject 를 발동할 것인지?")]
    public bool isReactObjectDontHaveItem = true;


    public void Start()
    {
        if(answer == null)
        {
            Debug.LogError(this.gameObject.name + "의 answer 이 입력되지 않았습니다.");
            return;
        }
        CheckValid();
        InitNowInput();
        FindQInput();
        
    }
    public void FindQInput()
    {
        qChild = GetComponentInChildren<QChild>();
    }
    /*
    private void OnEnable()
    {
        switch (answerType)
        {
            case enum_AnswerType.Number:
                {
                    if(nowInput == null || nowInput.Length == 0)
                    {
                        InitNowInput();
                    }
                    answerString = "";
                    for(int i = 0; i < answer.Length; i++)
                    {
                        nowInput[i] = defaultAnswer;
                        answerString += nowInput[i];
                        break;
                    }
                    RefreshAnswerUI();
                    break;
                }
        } 
    }
    */
    private void RefreshAnswerUI()
    {
        answerString = "";
        foreach(var e in nowInput)
        {
            answerString += e;
        }
        /*
        for (int i = 0; i < nowInput.Length; i++)
        {
            answerString += nowInput[i];
        }
        */
        answerText.text = answerString;
    }


    private void QInputButtonLock(bool isLock)
    {
        foreach(var e in qChild.qInputs)
        {
            e.SetButtonLock(!isLock);
        }
        DefaultData.singleton.allButtonLock.SetActive_LockImage(isLock);
    }

    private void CheckValid()
    {
        if(answerType == enum_AnswerType.CheckItem && checktargetItem == null)
        {
            //에러 출력
            Debug.LogError(this.gameObject.name + "의 answerType 이 CheckItem 인데 checktargetItem 이 null 입니다.");
        }
    }
    private void InitNowInput()
    {
        nowInput = new string[answer.Length];

        if(answerType == enum_AnswerType.Match && (answer.Length != this.GetComponentsInChildren<QInput>().Length))
        {
            Debug.LogError(string.Format($"{this.gameObject.name} 의 answerType 은 Match 입니다. Match 는 반드시 정답의 숫자와 하위에 있는 QInput 의 개수가 동일해야 합니다. \n현재 정답의 숫자는 {answer.Length} 개이고, 하위에 있는 QInput 의 숫자는 {this.GetComponentsInChildren<QInput>().Length} 입니다."));
        }

        /*
        for(int i = 0; i < nowInput.Length; i++)
        {
            nowInput[i] = defaultAnswer;
        }
        RefreshAnswerUI();
        */
    }

    private bool IsAnswer()
    {
        if(answer.Length != nowInput.Length)
        {
            Debug.LogError(this.gameObject.name + "의 answer 의 길이와 nowInput 의 길이가 같지 않습니다.");
        }
        
                
        for(int i = 0; i < answer.Length; i++)
        {
            Debug.Log(string.Format($"answer[{i}] = {answer[i]} / nowInput[{i}] = {nowInput[i]}"));
            if (answer[i] != nowInput[i])
            {
                return false;
            }
        }

        return true;
    }

    public void CheckAnswer_Match()
    {
        if (IsAnswer())
        {
            if (doNotifyOnAnswer_StreamData && onAnswerStreamDatas != null)
            {
                CompleteStreamData();
            }
        }
    }

    private IEnumerator CheckAnswer_Number()
    {
        //for test
        Debug.Log(string.Format($"Step 2 // 코루틴 시작 / 모든 버튼 잠금 시작"));

        QInputButtonLock(true);

        yield return DefaultData.singleton.qParentNumberCheckDelay;

        if (IsAnswer())
        {
            if (answerType == enum_AnswerType.Number)
            {
                Color tempAnswerTextColor = answerText.color;
                answerText.color = Color.blue;
                yield return DefaultData.singleton.qParentNumberCheckDelay;
            }
            QInputButtonLock(false);

            if (doNotifyOnAnswer_StreamData && onAnswerStreamDatas != null)
            {
                CompleteStreamData();
            }
        }
        else
        {
            if (answerType == enum_AnswerType.Number)
            {
                Color tempAnswerTextColor = answerText.color;
                answerText.color = Color.red;
                yield return DefaultData.singleton.qParentNumberCheckDelay;
                answerText.color = tempAnswerTextColor;
                OnInput_ClearNumber();
                QInputButtonLock(false);
            }
        }
    }

    public void OnInitMatch(int childIndex, string childInput)
    {
        if(nowInput == null || nowInput.Length == 0)
        {
            nowInput = new string[answer.Length];
        }
        nowInput[childIndex] = childInput;
    }

    public void OnInput(int childIndex, string childInput)
    {
        switch (answerType)
        {
            case enum_AnswerType.Default:
                {
                    Debug.LogError(this.gameObject.name + "의 answerType 이 Default 입니다. 값 설정이 필요합니다.");
                    break;
                }
            case enum_AnswerType.Number:
                {
                    //for test
                    Debug.Log(string.Format($"Step 0 // 버튼 입력 / 정답의 개수 = " + answer.Length + " // nowIndex = " + GetIndexOfNowNumber()));
                    OnInput_AnswerIsNumber(inputNumber: childInput);
                    if (GetIndexOfNowNumber() == -1)
                    {
                        //for test
                        Debug.Log(string.Format($"Step 1 // answer.Length = {answer.Length} 그리고 nowIndex = {GetIndexOfNowNumber()} 이므로 답 체크 시작"));
                        StartCoroutine(CheckAnswer_Number());
                    }
                    break;
                }
            case enum_AnswerType.Click:
                {
                    OnInput_Click();
                    break;
                }
            case enum_AnswerType.CheckItem:
                {
                    UseItem();
                    break;
                }
            case enum_AnswerType.Match:
                {
                    OnInput_AnswerIsMatch(childIndex, childInput);
                    CheckAnswer_Match();
                    break;
                }
            default:
                {
                    Debug.LogError(string.Format($"{answerType.ToString()}의 OnInput 이 구현되지 않았습니다. 구현해주세요."));
                    break;
                }
        }

        
    }
    private void OnInput_Click()
    {
        if (doNotifyOnAnswer_StreamData)
        {
            CompleteStreamData();
        }
    }
    public void OnInput_AnswerIsMatch(int childIndex, string inputString)
    {
        nowInput[childIndex] = inputString;
        //RefreshAnswerUI();
    }
    public void OnInput_ClearNumber()
    {
        for(int i = 0; i<nowInput.Length; i++)
        {
            nowInput[i] = defaultAnswer;
        }
        RefreshAnswerUI();
    }
    public void OnInput_AnswerIsNumber(string inputNumber)
    {
        int nowIndex = GetIndexOfNowNumber();
        if (nowIndex == -1)
        {
            //for test
            //Debug.Log(this.gameObject.name + "의 GetIndexOfNowNumber 가 -1 이다. 그에따른 처리 필요.");

            InitNowInput();
            OnInput_AnswerIsNumber(inputNumber);
        }
        else
        {
            nowInput[nowIndex] = inputNumber;
        }
        RefreshAnswerUI();
    }
    private int GetIndexOfNowNumber()
    {
        for(int i = 0; i < nowInput.Length; i++)
        {
            if(nowInput[i] == "" || nowInput[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    private void CompleteStreamData()
    {
        for (int i = 0; i < onAnswerStreamDatas.Count; i++) onAnswerStreamDatas[i].CompleteStream();
    }

    public void UseItem()
    { 
        StreamItem inventoryItem;
        try
        {
            inventoryItem = FindObjectOfType<Inventory>().m_nowSelectedItem.itemData as StreamItem;
        }
        catch(System.Exception exception)
        {
            Debug.Log("선택된 아이템이 없습니다.");
            if (isReactObjectDontHaveItem)
            {
                foreach (var e in qChild.qInputs)
                {
                    e.DoReact();
                }
            }
            return;
        }
        

        
        if (FindObjectOfType<Inventory>().m_nowSelectedItem.itemData as StreamItem == checktargetItem)
        {
            //for test
            Debug.Log("선택된 아이템 합격, 스트림 데이터 발동!");
            CompleteStreamData();
        }
    }
}
