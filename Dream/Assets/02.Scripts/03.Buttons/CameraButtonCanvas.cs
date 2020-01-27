using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtonCanvas : MonoBehaviour
{
    [Header("+ 캔버스 소속 친구들")]
    [SerializeField] List<GameObject> childObj;
    public static CameraButtonCanvas singleton;
    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    private void Start()
    {
        InitChild();

    }
    private void InitChild()
    {
        CameraButtonUI[] tempArr = this.GetComponentsInChildren<CameraButtonUI>();
        for (int i = 0; i < tempArr.Length; i++)
        {
            childObj.Add(tempArr[i].gameObject);
        }
        HideAllUI();
    }
    private void HideAllUI()
    {
        foreach(var e in childObj)
        {
            e.SetActive(false);
        }
    }
    public void ShowButtonUI(CameraObj nowCam)
    {
        HideAllUI();
        (childObj.Find(x => x.name == "bui_" + nowCam.gameObject.name)).SetActive(true);
    }
}
