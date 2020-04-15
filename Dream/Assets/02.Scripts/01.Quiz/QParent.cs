using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QParent : MonoBehaviour
{
    [Header("+ 정답의 타입")]
    public enum_AnswerType answerType = enum_AnswerType.Default;

    [Header("+ 정답 입력")]
    public string[] answer;

    [Header("+ 정답 시 발동할 StreamData")]
    public bool doNotifyOnAnswer_StreamData = false;
    public List<StreamData> onAnswerStreamDatas;

    [Header("+ 현재 입력된 값 (미입력 영역)")]
    [SerializeField] string[] nowInput;

    [Header("+ 체크할 아이템")]
    public StreamItem checktargetItem;
    

    public void Start()
    {
        if(answer == null)
        {
            Debug.LogError(this.gameObject.name + "의 answer 이 입력되지 않았습니다.");
            return;
        }
        CheckValid();
        InitNowInput();
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
    private void CheckAnswer()
    {
        if (IsAnswer())
        {
            //for test
            Debug.Log("정답 처리를 이곳에서 함");

            if (doNotifyOnAnswer_StreamData && onAnswerStreamDatas != null)
            {
                CompleteStreamData();
            }

            return;
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
                    OnInput_AnswerIsNumber(inputNumber: childInput);
                    CheckAnswer();
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
                    CheckAnswer();
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
    }
    public void OnInput_AnswerIsNumber(string inputNumber)
    {
        int nowIndex = GetIndexOfNowNumber();
        if (nowIndex == -1)
        {
            //for test
            Debug.Log(this.gameObject.name + "의 GetIndexOfNowNumber 가 -1 이다. 그에따른 처리 필요.");

            InitNowInput();
            OnInput_AnswerIsNumber(inputNumber);
        }
        else
        {
            nowInput[nowIndex] = inputNumber;
        }
        
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
        StreamItem inventoryItem = FindObjectOfType<Inventory>().m_nowSelectedItem.itemData as StreamItem;
        if (inventoryItem == null)
        {
            Debug.Log("선택된 아이템이 없습니다.");
            return;
        }
        if (inventoryItem == checktargetItem)
        {
            //for test
            Debug.Log("선택된 아이템 합격, 스트림 데이터 발동!");
            CompleteStreamData();
        }
    }
}
