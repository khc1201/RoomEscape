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

        //InitCameraList();
        //InitCamera();
    }
    [Header("+ 초기화 정보")]
    public GameObject m_cameraParent;
    public List<CameraObj> list_camobj;

    [Header("+ 현재 카메라")]
    //현재 카메라 정보 : for test
    public CameraObj m_nowCamera;

    [Header("+ 카메라 조이스틱 정보")]
    //카메라 조이스틱 정보
    public StreamShowTarget m_btn_forward;
    public StreamShowTarget m_btn_back;
    public StreamShowTarget m_btn_right;
    public StreamShowTarget m_btn_left;
    public void LoadCamera()
    {
        InitCameraList();
        InitCamera();
    }
    
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
        m_nowCamera = list_camobj.Find(x => x.gameObject.name == UserData.singleton.m_nowCam);
        
        if(m_nowCamera == null)
        {
            Debug.LogError(string.Format($"{UserData.singleton.m_nowCam} 에 대한 카메라 정보를 list_camobj 에서 찾지 못했습니다."));
            return;
        }
        m_nowCamera.enabled = true;
        RefreshCameraMoveButtonUI();
        RefreshCameraButtonsUI();
        LevelManager.singleton.InitLevelOptimization();
    }

    public void ChangeCamera(CameraObj precam, CameraObj nextcam)
    {
        precam.enabled = false;
        nextcam.enabled = true;
        m_nowCamera = nextcam;
        RefreshCameraMoveButtonUI();
        RefreshCameraButtonsUI();
        UserData.singleton.SetNowCam(m_nowCamera.gameObject.name);
        //종료할때만 nowCamera 와 Data 정보들을 저장한다?

        if(precam.m_levelOptimizationIndex != nextcam.m_levelOptimizationIndex)
        {
            //for test
            Debug.Log(string.Format($"최적화 레벨 변경됨 : {precam.m_levelOptimizationIndex} -> {nextcam.m_levelOptimizationIndex}"));

            LevelManager.singleton.ChangeLevelOptimization(nextcam.m_levelOptimizationIndex);
        }
    }
    private void RefreshCameraButtonsUI()
    {
        CameraButtonCanvas.singleton.ShowButtonUI(m_nowCamera);
            //  m_nowCamera.gameObject.name
    }

    public void RefreshCameraMoveButtonUI()
    {

        m_btn_forward.TargetShow(m_nowCamera.m_dircamobj_forward != null ? true : false);
        m_btn_back.TargetShow(m_nowCamera.m_dircamobj_back != null ? true : false);
        m_btn_left.TargetShow(m_nowCamera.m_dircamobj_left != null ? true : false);
        m_btn_right.TargetShow(m_nowCamera.m_dircamobj_right != null ? true : false);
        
        /*
        m_btn_forward.SetButton(m_nowCamera.m_dircamobj_forward != null);
        m_btn_back.SetButton(m_nowCamera.m_dircamobj_back != null);
        m_btn_right.SetButton(m_nowCamera.m_dircamobj_right != null);
        m_btn_left.SetButton(m_nowCamera.m_dircamobj_left != null);
        */
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
