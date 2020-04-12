using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveButtonStreamShow : MonoBehaviour
{
    CameraObj thisCam;
    CameraManager camManager;

    public bool isShowOnInit_forward = true;
    public List<StreamShowList> StreamShow_forward;

    public bool isShowOnInit_back = true;
    public List<StreamShowList> StreamShow_back;

    public bool isShowOnInit_right = true;
    public List<StreamShowList> StreamShow_right;

    public bool isShowOnInit_left = true;
    public List<StreamShowList> StreamShow_left;


    private void Start()
    {
        Init();
    }
    private void Init()
    {
        thisCam = GetComponent<CameraObj>();
        camManager = CameraManager.singleton;

        if(thisCam == null)
        {
            Debug.LogError(this.gameObject.name + "에서 thisCam 이 null 임");
            return;
        }
        /*
        if((StreamShow_forward.Count != 0 || StreamShow_forward != null) && thisCam.m_dircamobj_forward == null)
        {
            Debug.LogError(this.gameObject.name + "의 ButtonStreamShow 값은 입력되어 있지만 정작 m_dircamobj_forward 값이 없음. 입력 오류");
            return;
        }

        if ((StreamShow_back.Count != 0 || StreamShow_back != null) && thisCam.m_dircamobj_back == null)
        {
            Debug.LogError(this.gameObject.name + "의 ButtonStreamShow 값은 입력되어 있지만 정작 m_dircamobj_back 값이 없음. 입력 오류");
            return;
        }

        if ((StreamShow_right.Count != 0 || StreamShow_right != null) && thisCam.m_dircamobj_right == null)
        {
            Debug.LogError(this.gameObject.name + "의 ButtonStreamShow 값은 입력되어 있지만 정작 m_dircamobj_right 값이 없음. 입력 오류");
            return;
        }

        if ((StreamShow_left.Count != 0 || StreamShow_left != null) && thisCam.m_dircamobj_left == null)
        {
            Debug.LogError(this.gameObject.name + "의 ButtonStreamShow 값은 입력되어 있지만 정작 m_dircamobj_left 값이 없음. 입력 오류");
            return;
        }
        */
    }
    /*
    private void OnEnable()
    {
        TargetShowOnCameraMove();
    }
    */
    public void TargetShowOnCameraMove()
    {
        if(thisCam.m_dircamobj_forward != null)
        {
            camManager.m_btn_forward.TargetShow(isShowOnInit_forward);
            
            if(StreamShow_forward != null)
            {
                for (int i = StreamShow_forward.Count - 1; i >= 0; i--)
                {
                    if (UserData.singleton.IsCompleteStream(StreamShow_forward[i].forStreamdata))
                    {
                        if (StreamShow_forward[i].showhide == enum_ShowHide.SHOW)
                        {
                            camManager.m_btn_forward.TargetShow(true);
                            break;
                        }
                        else
                        {
                            camManager.m_btn_forward.TargetShow(false);
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            camManager.m_btn_forward.TargetShow(false);
        }

        if(thisCam.m_dircamobj_back != null)
        {
            camManager.m_btn_back.TargetShow(isShowOnInit_back);

            if (StreamShow_back != null)
            {
                for (int i = StreamShow_back.Count - 1; i >= 0; i--)
                {
                    if (UserData.singleton.IsCompleteStream(StreamShow_back[i].forStreamdata))
                    {
                        if (StreamShow_back[i].showhide == enum_ShowHide.SHOW)
                        {
                            camManager.m_btn_back.TargetShow(true);
                            break;
                        }
                        else
                        {
                            camManager.m_btn_back.TargetShow(false);
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            camManager.m_btn_back.TargetShow(false);
        }

        if (thisCam.m_dircamobj_right != null)
        {

            camManager.m_btn_right.TargetShow(isShowOnInit_right);

            if (StreamShow_right != null)
            {
                for (int i = StreamShow_right.Count - 1; i >= 0; i--)
                {
                    if (UserData.singleton.IsCompleteStream(StreamShow_right[i].forStreamdata))
                    {
                        if (StreamShow_right[i].showhide == enum_ShowHide.SHOW)
                        {
                            camManager.m_btn_right.TargetShow(true);
                            break;
                        }
                        else
                        {
                            camManager.m_btn_right.TargetShow(false);
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            camManager.m_btn_right.TargetShow(false);
        }

        if (thisCam.m_dircamobj_left != null)
        {
            camManager.m_btn_left.TargetShow(isShowOnInit_left);

            if (StreamShow_left != null)
            {
                for (int i = StreamShow_left.Count - 1; i >= 0; i--)
                {
                    if (UserData.singleton.IsCompleteStream(StreamShow_left[i].forStreamdata))
                    {
                        if (StreamShow_left[i].showhide == enum_ShowHide.SHOW)
                        {
                            camManager.m_btn_left.TargetShow(true);
                            break;
                        }
                        else
                        {
                            camManager.m_btn_left.TargetShow(false);
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            camManager.m_btn_left.TargetShow(false);
        }
    }




}
