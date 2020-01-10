using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager singleton;
    
    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
    }

    private void Start()
    {
        if (m_cameraParent == null) Debug.LogError(string.Format($"{this.gameObject.name} 의 m_cameraParent 가 null 값"));

        InitCameraList();
        ForTestCameraInit();
        InitCamera();
    }
    [Header("+ 초기화 정보")]
    public GameObject m_cameraParent;
    public List<CameraObj> list_camobj;

    [Header("+ 현재 카메라")]
    //현재 카메라 정보 : for test
    public CameraObj m_nowCamera;

    [Header("+ 카메라 조이스틱 정보")]
    //카메라 조이스틱 정보
    public CameraMoveButton m_btn_forward;
    public CameraMoveButton m_btn_back;
    public CameraMoveButton m_btn_right;
    public CameraMoveButton m_btn_left;

    
    private void InitCameraList()
    {

        list_camobj = new List<CameraObj>();

        foreach (var e in m_cameraParent.GetComponentsInChildren<CameraObj>())
        {
            list_camobj.Add(e);
        }
    }
    private void InitCamera()
    {
        CameraObj temp_initCam = list_camobj.Find(x => x.gameObject.name == UserData.singleton.m_nowCam);
        if(temp_initCam == null)
        {
            Debug.LogError(string.Format($"{UserData.singleton.m_nowCam} 에 대한 카메라 정보를 list_camobj 에서 찾지 못했습니다."));
            return;
        }
        temp_initCam.enabled = true;
        RefreshCameraMoveButtonUI();
    }
    public void ChangeCamera(CameraObj precam, CameraObj nextcam)
    {
        precam.enabled = false;
        nextcam.enabled = true;
        m_nowCamera = nextcam;
        RefreshCameraMoveButtonUI();
        UserData.singleton.SetNowCam(m_nowCamera.gameObject.name);
        //종료할때만 nowCamera 와 Data 정보들을 저장한다?
    }
    private void ForTestCameraInit()
    {
        ChangeCamera(list_camobj[1], list_camobj[0]);
    }

    public void RefreshCameraMoveButtonUI()
    {
        m_btn_forward.SetButton(m_nowCamera.m_dircamobj_forward != null);
        m_btn_back.SetButton(m_nowCamera.m_dircamobj_back != null);
        m_btn_right.SetButton(m_nowCamera.m_dircamobj_right != null);
        m_btn_left.SetButton(m_nowCamera.m_dircamobj_left != null);
    }
    public void CameraMove(MoveDir direction)
    {
        switch (direction)
        {
            case MoveDir.Forward:
                {
                    if (m_nowCamera.m_dircamobj_forward == null)
                    {
                        Debug.LogError(string.Format($"{this.gameObject.name} 의 forward camera 가 null 입니다."));
                        return;
                    }

                    ChangeCamera(m_nowCamera, m_nowCamera.m_dircamobj_forward);

                    break;
                }
            case MoveDir.Left:
                {
                    if (m_nowCamera.m_dircamobj_left == null)
                    {
                        Debug.LogError(string.Format($"{this.gameObject.name} 의 left camera 가 null 입니다."));
                        return;
                    }

                    ChangeCamera(m_nowCamera, m_nowCamera.m_dircamobj_left);

                    break;
                }
            case MoveDir.Back:
                {
                    if (m_nowCamera.m_dircamobj_back == null)
                    {
                        Debug.LogError(string.Format($"{this.gameObject.name} 의 back camera 가 null 입니다."));
                        return;
                    }

                    ChangeCamera(m_nowCamera, m_nowCamera.m_dircamobj_back);

                    break;
                }
            case MoveDir.Right:
                {
                    if (m_nowCamera.m_dircamobj_right == null)
                    {
                        Debug.LogError(string.Format($"{this.gameObject.name} 의 right camera 가 null 입니다."));
                        return;
                    }

                    ChangeCamera(m_nowCamera, m_nowCamera.m_dircamobj_right);

                    break;
                }
        }
    }
}
