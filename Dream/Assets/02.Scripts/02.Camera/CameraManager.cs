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

    //현재 카메라 정보
    public Camera m_nowCamera;

    public void ChangeCamera(CameraObj precam, CameraObj nextcam)
    {
        
    } 


}
