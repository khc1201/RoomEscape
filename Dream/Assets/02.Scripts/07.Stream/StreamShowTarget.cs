using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamShowTarget : MonoBehaviour
{
    public GameObject targetObject;
    public bool isShowOnInit = true;
    public List<StreamShowList> streamList; // 이것을 List로 제어하는 이유는, 어떤 CompleteStream 이 가장 나중에 이루어졌을 지 모르기 때문에...

    private void Start()
    {
        if(this.transform.childCount != 1)
        {
            Debug.LogError(this.gameObject.name + "의 targetObject의 개수가 1 이 아니다. 반드시 1이어야 함");
            return;
        }

        targetObject = this.transform.GetChild(0).gameObject;

        /*
        if (this.transform.GetComponentInChildren<CameraMoveButton>() == null)
        {
            //for test
            Debug.Log(this.gameObject.name + "의 방향버튼 Show Hide 시작!");
            TargetShowOnCameraMove(this.GetComponentInChildren<CameraMoveButton>().m_moveDir, false);
        }
        */
        if (this.transform.GetComponentInChildren<CameraMoveButton>() == null)
        {
            if (!isShowOnInit)
            {
                TargetShow(false);
            }

            TargetShowOnAction();
        }

    }

    public void TargetShow(bool isShow)
    {
        //for test
        //Debug.Log("targetShow 가 발동 됨 = " + isShow.ToString());

        targetObject.SetActive(isShow);
    }

    private void OnEnable()
    {
        TargetShowOnAction();
    }

    private void TargetShowOnAction()
    {
        //for test
        //Debug.Log(this.gameObject.name + "의 OnEnable 에 따라서 TargetShow를 판단해보자!");

        if (streamList == null)
        {
            Debug.Log(this.gameObject.name + "에는 streamList 가 설정되어 있지 않음. return.");
            return;
        }
        

        // StreamList 가 설정되어 있을 경우, 레벨이나 데이터 로딩 시에 streamList 에 있는 Show 기능을 수행한다.
        for (int i = streamList.Count - 1; i >= 0; i--)
        {
            if (UserData.singleton.IsCompleteStream(streamList[i].forStreamdata))
            {
                //for test
                //Debug.Log(this.gameObject.name + "에 설정된 Streamlist 가 userdata 의 Complete List 에 존재하므로 관련 작업을 수행한다.");

                if (streamList[i].showhide == enum_ShowHide.SHOW)
                {
                    this.TargetShow(true);
                    return;
                }
                else
                {
                    this.TargetShow(false);
                    return;
                }
            }
        }
    }
}
