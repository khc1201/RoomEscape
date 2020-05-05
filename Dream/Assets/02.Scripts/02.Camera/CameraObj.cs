using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObj : MonoBehaviour
{
    [Header("+ 기본 정보")]
    [SerializeField]
    private Camera m_cam;
    [SerializeField]
    private CameraButtonUI m_ui;

    [Header("+ 방향 조작에 따른 카메라 이동")]
    public CameraObj m_dircamobj_forward;
    public CameraObj m_dircamobj_left;
    public CameraObj m_dircamobj_back;
    public CameraObj m_dircamobj_right;

    [Header("+ 레벨 최적화 인덱스")]
    public string m_levelOptimizationIndex;

    private void Awake()
    {
        FindCamera();
        CameraResolution();
    }

    void CameraResolution()
    {

        Rect rect = m_cam.rect;
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float scaleWidth = 1f / scaleHeight;
        if(scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        m_cam.rect = rect;
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckLevelOptimizationIndex();
    }

    private void OnDisable()
    {
        SetEnable(false);
    }


    private void OnEnable()
    {
        if(m_ui == null)  FindCameraButtonUI();
        if(m_cam == null) FindCamera();

        SetEnable(true);
    }

    private void SetEnable(bool isEnable)
    {
        m_ui.enabled = isEnable;
        m_cam.transform.GetComponent<AudioListener>().enabled = isEnable;
        m_cam.enabled = isEnable;
    }

    private void FindCameraButtonUI()
    {
        m_ui = (FindObjectOfType<CameraButtonCanvas>().gameObject.transform.Find("bui_" + this.gameObject.name)).transform.GetComponent<CameraButtonUI>();
    }
    private void FindCamera()
    {
        m_cam = transform.GetComponentInChildren<Camera>();
    }

    private void CheckLevelOptimizationIndex()
    {
        if(m_levelOptimizationIndex == null || m_levelOptimizationIndex == "")
        {
            Debug.LogError(this.gameObject.name + " 의 m_levelOptimizationIndex 가 정해지지 않았다");
            return;
        }
    }
}
