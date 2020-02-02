using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamObject : MonoBehaviour
{
    [Header(" + 설명")]
    public string descripiton;

    [Header(" + 이것이 완료된 후에 다음 StreamObject 를 재생할 것인지?")]
    public bool isplayafterComplete = true;
    public bool IsEnd { get { return reactObject.IsEnd; } }
    public bool IsIgnoreOnLoad { get { return reactObject.IsIgnoreOnLoad; } }


    [Header(" + ReactObject")]
    [SerializeField] private ReactObject reactObject;

    public void Start()
    {

        reactObject = this.GetComponent<ReactObject>();
        if(reactObject == null)
        {
            Debug.LogError(this.gameObject.name + "의 reactObject 가 null 값입니다.");
            return;
        }
        if (reactObject.isQInputObject) Debug.LogError(reactObject.name + "의 isQInputObject 가 true 이다. 어디에서 체크 된 것인지 확인필요.");
        reactObject.isStreamObject = true;

    }

    public void DoAction()
    {
        reactObject.DoAction(isplayafterComplete);
    }
}
