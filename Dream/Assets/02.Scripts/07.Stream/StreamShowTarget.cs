using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamShowTarget : MonoBehaviour
{
    public GameObject targetObject;
    private void Start()
    {
        if(this.transform.childCount != 1)
        {
            Debug.LogError(this.gameObject.name + "의 targetObject의 개수가 1 이 아니다. 반드시 1이어야 함");
            return;
        }
        targetObject = this.transform.GetChild(0).gameObject;
    }

    public void TargetShow(bool isShow)
    {
        //for test
        Debug.Log("targetShow 가 발동 됨 = " + isShow.ToString());

        targetObject.SetActive(isShow);
    }
}
