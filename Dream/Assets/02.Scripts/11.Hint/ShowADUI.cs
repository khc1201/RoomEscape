using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hint;


public class ShowADUI : MonoBehaviour
{
    public string str_wantShowAD;
    public string str_yes;

    public Text txt_wantShowAD;
    public Text txt_yes;

    public Button btn_yes;
    public Button btn_exit;

    private Vector3 initPosY = new Vector3(0,1500f);
    private HintSlot targetHintSlot;

    private void Start()
    {
        InitButtons();
    }
    private void InitButtons()
    {
        btn_yes.onClick.RemoveAllListeners();
        btn_exit.onClick.RemoveAllListeners();

        btn_yes.onClick.AddListener(OnYesButtonClick);
        btn_exit.onClick.AddListener(OnNoButtonClick);
        
    }

    public void OnYesButtonClick()
    {
        ShowAD();
    }
    public void OnNoButtonClick()
    {
        ShowADBox(false);
    }

    private void SetText()
    {
        txt_wantShowAD.text = StringDataManager.singleton.GetText(str_wantShowAD);
        txt_yes.text = StringDataManager.singleton.GetText(str_yes);
    }

    public void ShowADBox(bool isShow = true)
    {
        if (isShow)
        {
            DefaultData.singleton.allButtonLock.SetActive_LockImage(true);
            SetText();
            this.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
        else
        {
            DefaultData.singleton.allButtonLock.SetActive_LockImage(false);
            this.GetComponent<RectTransform>().localPosition = initPosY; 
        }
    }

    public void CheckShowAD(HintSlot targetSlot)
    {
        targetHintSlot = targetSlot;
        ShowADBox(true);

    }

    public void ShowAD()
    {
        //for test 이곳에서 광고를 재생
        //현재는 테스트용으로, 딜레이를 주기 위해 임시로 Temp() 함수를 추가
        ShowADBox(false);
        targetHintSlot.hintObject.isOpenned = true;
        targetHintSlot.hintObject.ShowHint();
        targetHintSlot.RefreshButtonUI();
        //for test
        Debug.Log("광고 보기 완료!");
        targetHintSlot = null;
    }

}
